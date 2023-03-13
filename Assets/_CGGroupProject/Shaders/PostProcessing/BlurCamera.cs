using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class BlurCamera : MonoBehaviour
{
    public Shader dofShader;
    [NonSerialized]
    Material dofMaterial;
    const int circleOfConfusionPass = 1;
    const int bokehPass = 1;

    [Range(0.1f, 100f)]
    public float focusDistance = 10f;
    [Range(0.1f, 10f)]
    public float focusRange = 3f;

    void OnRenderImage(RenderTexture source, RenderTexture
    destination)
    {
        if (dofMaterial == null)
        {
            dofMaterial = new Material(dofShader);
            dofMaterial.hideFlags = HideFlags.HideAndDontSave;
        }

        dofMaterial.SetFloat("_FocusDistance", focusDistance);
        dofMaterial.SetFloat("_FocusRange", focusRange);
        RenderTexture coc = RenderTexture.GetTemporary(
            source.width, source.height, 0,
            RenderTextureFormat.RHalf,
            RenderTextureReadWrite.Linear
            );
        Graphics.Blit(source, destination, dofMaterial, circleOfConfusionPass);
        //Graphics.Blit(dof0, dof1, dofMaterial, bokehPass);
        //Graphics.Blit(coc, destination);
        RenderTexture.ReleaseTemporary(coc);
    }
}
