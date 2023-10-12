using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_UnparentChildren : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DetachChildren();
    }
}
