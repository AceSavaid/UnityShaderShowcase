using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLUT : MonoBehaviour
{
    //public Shader awesomeShader = null;
    public Material LUTMaterial;
    //public Material OutlineMaterial;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, LUTMaterial);
        //Graphics.Blit(source, destination, OutlineMaterial);
    }
}
