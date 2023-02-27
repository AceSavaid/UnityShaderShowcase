using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class COutlinePP : MonoBehaviour
{
    public Shader OutlineShader;
    public Material OutlineMat;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (OutlineMat == null) {
            OutlineMat = new Material(OutlineMat);
            OutlineMat.hideFlags = HideFlags.HideAndDontSave;
        }

        Graphics.Blit(source, destination, OutlineMat);
    }
    
}
