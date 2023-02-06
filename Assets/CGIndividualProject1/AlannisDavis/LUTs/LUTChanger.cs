using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUTChanger : MonoBehaviour
{
    ALUTCamera camerasettings;
    public List<Material> luts = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
        camerasettings = GetComponent<ALUTCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            camerasettings.LUTMaterial = luts[0];
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            camerasettings.LUTMaterial = luts[1];
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            camerasettings.LUTMaterial = luts[2];
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            camerasettings.LUTMaterial = luts[3];
        }
    }
}
