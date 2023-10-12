using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TrackPicker : MonoBehaviour
{
    [SerializeField]
    GameObject[] tracks;
    void Awake()
    {
        Instantiate(tracks[Random.Range(0, tracks.Length)]);
    }
}
