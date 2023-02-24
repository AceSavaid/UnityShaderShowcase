using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentChangeZone : MonoBehaviour
{
    [SerializeField] Material LUT;
    private void OnTriggerEnter(Collider other)
    {
        //set lut of camera to lut in this script

    }
}
