using NWH.Common.Vehicles;
using NWH.WheelController3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_CarController : MonoBehaviour
{

    //add a title in the inspector
    [Header("Car Settings")]
    public float maxMotorTorque = 1000f;
    public float maxSteeringAngle = 35f;
    public float minSteeringAngle = 20f;
    public float maxBrakeTorque = 3000f;
    public float steeringSpeed = 0.5f;

    [Header("Boost Settings")]
    public float boostMultiplier = 2f;
    private bool isBoosted = false;
    [Header("List Of Wheels")]
    public List<WheelController> wheels;

    [Header("Input Mapping")]
    [SerializeField]
    public InputActionAsset inputActionAsset;

    private InputAction moveAction;
    private InputAction steerAction;
    private Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null) gameObject.AddComponent<Rigidbody>();
    }

    public void ApplyBoost(float boostDuration)
    {
        if (!isBoosted)
        {
            StartCoroutine(BoostCoroutine(boostDuration));
        }
    }

    private IEnumerator BoostCoroutine(float boostDuration)
    {
        isBoosted = true;
        yield return new WaitForSeconds(boostDuration);
        isBoosted = false;
    }

    private void OnEnable()
    {
        var carActionMap = inputActionAsset.FindActionMap("Car");
        moveAction = carActionMap.FindAction("Move");
        steerAction = carActionMap.FindAction("Steer");

        moveAction.Enable();
        steerAction.Enable();
    }

    private void OnDisable()
    {
        //moveAction.Disable();
        //steerAction.Disable();
    }

    private void FixedUpdate()
    {
        float motorInput = moveAction.ReadValue<float>();
        float steerInput = steerAction.ReadValue<float>();

        float currentMotorTorque = maxMotorTorque;
        if (isBoosted)
        {
            currentMotorTorque *= boostMultiplier;
        }

        for (int i = 0; i < wheels.Count; i++)
        {
            WheelUAPI wc = wheels[i];

            if (motorInput > 0)
            {
                wc.MotorTorque = currentMotorTorque * motorInput;
                wc.BrakeTorque = 0f;
            }
            else if (motorInput < 0 && _rigidbody.velocity.magnitude > 0.1f && _rigidbody.velocity.z > 0f)
            {
                wc.MotorTorque = 0f;
                wc.BrakeTorque = maxBrakeTorque * Mathf.Abs(motorInput);
            }
            else
            {
                wc.MotorTorque = currentMotorTorque * motorInput;
                wc.BrakeTorque = 0f;
            }

            wc.SteerAngle = Mathf.Lerp(maxSteeringAngle, minSteeringAngle, steeringSpeed) * steerInput;

            
        }
    }
}