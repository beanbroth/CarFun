using Cinemachine;
using NWH.WheelController3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_JumpController : MonoBehaviour
{
    public List<WheelController> wheelControllers = new List<WheelController>();
    private bool anyWheelCurrentlyGrounded;
    private bool anyWheelPreviouslyNotGrounded;
    private bool hasLeftGround;
    private Rigidbody vehicleRigidbody;
    public float forwardForce = 10f;
    public float pitchTorque = 5f;
    public float steerTorque = 5f;

    private InputActionAsset inputActionAsset;
    private InputAction moveAction;
    private InputAction steerAction;

    public S_CarController carController;
    public ParticleSystem  jumpParticles;

    public CinemachineVirtualCamera jumpCam;

    private void Start()
    {
        vehicleRigidbody = GetComponent<Rigidbody>();
        carController = GetComponent<S_CarController>();
        inputActionAsset = carController.inputActionAsset;
        var carActionMap = inputActionAsset.FindActionMap("Car");
        moveAction = carActionMap.FindAction("Move");
        steerAction = carActionMap.FindAction("Steer");

        moveAction.Enable();
        steerAction.Enable();
    }

    private void OnDisable()
    {
        if (moveAction == null || steerAction == null)
        {
            return;
        }
        moveAction.Disable();
        steerAction.Disable();
    }

    private void FixedUpdate()
    {
        UpdateGroundedState();
    }

    private void UpdateGroundedState()
    {
        bool isAnyWheelGrounded = false;
        foreach (WheelController wheelController in wheelControllers)
        {
            if (wheelController.IsGrounded)
            {
                isAnyWheelGrounded = true;
                carController.enabled = true;
                jumpCam.Priority = -1;
                jumpParticles.Stop();
                jumpParticles.Clear();

                break;
            }
        }

        if (anyWheelPreviouslyNotGrounded && !isAnyWheelGrounded && !hasLeftGround)
        {
            hasLeftGround = true;
            jumpCam.Priority = 100;
        }

        if (!anyWheelCurrentlyGrounded && !isAnyWheelGrounded)
        {
            if (hasLeftGround)
            {
                vehicleRigidbody.angularVelocity = Vector3.zero;
                hasLeftGround = false;

            }

            carController.enabled = false;
            jumpParticles.Play();

            foreach (WheelController wheelController in wheelControllers)
            {
                wheelController.BrakeTorque = 0;
                wheelController.MotorTorque = 0;
            }

            vehicleRigidbody.AddForce(transform.forward * forwardForce, ForceMode.Force);
            vehicleRigidbody.AddForce(Physics.gravity, ForceMode.Acceleration);

            float moveInput = moveAction.ReadValue<float>();
            Vector3 pitchTorqueVector = transform.right * moveInput * pitchTorque;
            //vehicleRigidbody.AddTorque(pitchTorqueVector, ForceMode.Force);

            float steerInput = steerAction.ReadValue<float>();
            Vector3 steerTorqueVector = transform.up * steerInput * steerTorque;
            //vehicleRigidbody.AddTorque(steerTorqueVector, ForceMode.Force);

            vehicleRigidbody.angularVelocity = steerTorqueVector;
        }

        if (isAnyWheelGrounded && !anyWheelCurrentlyGrounded)
        {

            vehicleRigidbody.velocity = vehicleRigidbody.velocity * 0.5f;
            hasLeftGround = false;
        }



        anyWheelPreviouslyNotGrounded = !isAnyWheelGrounded;
        anyWheelCurrentlyGrounded = isAnyWheelGrounded;
    }
}