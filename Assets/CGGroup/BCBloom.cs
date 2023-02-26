using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class BCBloom : MonoBehaviour
{
    // Start is called before the first frame update
    const int BoxDownPass = 0;
    const int BoxUpPass = 1;
    const int ApplyBloomPass = 2;

    public Shader bloomShader;
    [Range(1, 16)]
    public int iterations = 4;

    [Range(0, 10)]
    public float threshold = 1;

    RenderTexture[] textures = new RenderTexture[16];

    [NonSerialized]
    Material bloom;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (bloom == null)
        {
            bloom = new Material(bloomShader);
            bloom.hideFlags = HideFlags.HideAndDontSave;
        }
        int width = source.width / 2;
        int height = source.height / 2;

        RenderTextureFormat format = source.format;
        RenderTexture currentDestination = textures[0] =
                RenderTexture.GetTemporary(width, height, 0, format);
        Graphics.Blit(source, currentDestination, bloom, BoxDownPass);
        RenderTexture currentSource = currentDestination;
        int i = 1;
        for (; i < iterations; i++)
        {
            width /= 2;
            height /= 2;
            if (height < 2)
            {
                break;
            }
            currentDestination = textures[i] =
                RenderTexture.GetTemporary(width, height, 0, format);
            Graphics.Blit(currentSource, currentDestination, bloom, BoxDownPass);
            currentSource = currentDestination;
        }
        for (i -= 2; i >= 0; i--)
        {
            currentDestination = textures[i];
            textures[i] = null;
            Graphics.Blit(currentSource, currentDestination, bloom,BoxUpPass);
            RenderTexture.ReleaseTemporary(currentSource);
            currentSource = currentDestination;
        }
        Graphics.Blit(currentSource, destination, bloom,ApplyBloomPass);

        RenderTexture.ReleaseTemporary(currentSource);
    }
}