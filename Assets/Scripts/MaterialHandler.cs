using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialHandler : MonoBehaviour
{
    [SerializeField] List<Material> materials = new List<Material>();
    Material originalMat;
    // Start is called before the first frame update
    void Start()
    {
        originalMat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void ChangeMaterial(int index)
    {
        if (materials[index] != null) {
            gameObject.GetComponent<MeshRenderer>().material = materials[index];
        }
        
    }

    private void Reset()
    {
        gameObject.GetComponent<MeshRenderer>().material = originalMat;
    }
}
