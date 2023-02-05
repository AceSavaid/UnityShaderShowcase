using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALUTCamera : MonoBehaviour
{
    //public Shader awesomeShader = null;
    public Material LUTMaterial;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, LUTMaterial);
    }
}
