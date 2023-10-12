using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerDeathBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindObjectOfType<GameManager>().TogglePause();
            GameObject.FindObjectOfType<GameManager>().PlayerDeath(other.transform.root.name);
        }
    }
}
