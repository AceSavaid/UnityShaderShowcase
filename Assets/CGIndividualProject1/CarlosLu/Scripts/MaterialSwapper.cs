using System.Collections.Generic;
using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    [SerializeField] private List<Material> Materials;
    [SerializeField] private Renderer Renderer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Renderer.materials[3] = Materials[0];
        }
        
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Renderer.materials[3] = Materials[1];
        }
        
        if (Input.GetKey(KeyCode.Alpha3))
        {
            Renderer.materials[3] = Materials[2];
        }
        
        if (Input.GetKey(KeyCode.Alpha4))
        {
            Renderer.materials[3] = Materials[3];
        }
        
        if (Input.GetKey(KeyCode.Alpha5))
        {
            Renderer.materials[3] = Materials[4];
        }
        
        if (Input.GetKey(KeyCode.Alpha6))
        {
            Renderer.materials[3]= Materials[5];
        }
        
        if (Input.GetKey(KeyCode.Alpha7))
        {
            Renderer.materials[3] = Materials[6];
        }
        
        if (Input.GetKey(KeyCode.Alpha8))
        {
            Renderer.materials[3] = Materials[7];
        }
        
        if (Input.GetKey(KeyCode.Alpha9))
        {
            Renderer.materials[3] = Materials[8];
        }
        
        if (Input.GetKey(KeyCode.Alpha0))
        {
            Renderer.materials[3] = Materials[9];
        }
    }
}
