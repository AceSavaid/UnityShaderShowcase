using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwap : MonoBehaviour
{
    [SerializeField] List<Material> materials = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Material 1 Selected");
            gameObject.GetComponent<MeshRenderer>().material = materials[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Material 2 Selected");
            gameObject.GetComponent<MeshRenderer>().material = materials[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Material 3 Selected");
            gameObject.GetComponent<MeshRenderer>().material = materials[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Material 4 Selected");
            gameObject.GetComponent<MeshRenderer>().material = materials[3];
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Material 5 Selected");
            gameObject.GetComponent<MeshRenderer>().material = materials[4];
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("Material 6 Selected");
            gameObject.GetComponent<MeshRenderer>().material = materials[5];
        }

    }
}
