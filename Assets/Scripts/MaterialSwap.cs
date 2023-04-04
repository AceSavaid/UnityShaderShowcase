using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialSwap : MonoBehaviour
{
    [SerializeField] List<Material> mMaterials = new List<Material>();
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
    
    public void ChangeMaterial(int index)
    {
        if (mMaterials[index] != null) {
            gameObject.GetComponent<MeshRenderer>().material = mMaterials[index];
        }
        
    }

    private void Reset()
    {
        gameObject.GetComponent<MeshRenderer>().material = originalMat;
    }
}
