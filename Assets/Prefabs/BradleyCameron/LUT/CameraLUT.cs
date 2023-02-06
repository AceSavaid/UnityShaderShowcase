using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLUT : MonoBehaviour
{
    //public Shader awesomeShader = null;
    public Material m_renderMaterial;
    public List<Material> LUTMaterials = new List<Material>();
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, m_renderMaterial);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            m_renderMaterial = LUTMaterials[0];
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            m_renderMaterial = LUTMaterials[1];
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            m_renderMaterial = LUTMaterials[2];
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            m_renderMaterial = LUTMaterials[3];
        }
    }
}
