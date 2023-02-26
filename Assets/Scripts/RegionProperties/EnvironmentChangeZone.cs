using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentChangeZone : MonoBehaviour
{
    [SerializeField] Material LUT;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            //set lut of camera to lut in this script
            FindObjectOfType<CameraLUTRenderer>().LUTMaterial = LUT;
        }
        
    }
}
