using UnityEngine;

public class S_SpeedPowerup : MonoBehaviour
{
    public float boostDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            S_CarController player = other.transform.root.GetComponent<S_CarController>();
            if (player != null)
            {
                player.ApplyBoost(boostDuration);
                Destroy(gameObject);
            }
        }
    }
}