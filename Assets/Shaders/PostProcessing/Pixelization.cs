using UnityEngine;

//[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class Pixelization : MonoBehaviour
{
    [SerializeField] private ComputeShader pixelizationCompute; 
    [Range(2, 40)] public int BlockSize = 3;

    int _screenWidth;
    int _screenHeight;
    bool isOn = true;
    public RenderTexture _renderTexture;  
    
    void CreateRenderTexture()
    {
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        
        _renderTexture = new RenderTexture(_screenWidth, _screenHeight, 24);
        _renderTexture.filterMode = FilterMode.Point;
        _renderTexture.enableRandomWrite = true;
        _renderTexture.Create();
    }

    void Update()
    {
        if (Screen.width != _screenWidth || Screen.height != _screenHeight && isOn)
            CreateRenderTexture();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, _renderTexture);

        int mainKernal = pixelizationCompute.FindKernel("Pixelation");
        pixelizationCompute.SetInt("_BlockSize", BlockSize);
        pixelizationCompute.SetInt("_ResultWidth", _renderTexture.width);
        pixelizationCompute.SetInt("_ResultHeight", _renderTexture.height);
        pixelizationCompute.SetTexture(mainKernal, "Result", _renderTexture);
        
        pixelizationCompute.GetKernelThreadGroupSizes(mainKernal, out uint xGroupSize, out uint yGroupSize, out _);
        pixelizationCompute.Dispatch(mainKernal,
            Mathf.CeilToInt(_renderTexture.width / (float)BlockSize / xGroupSize),
            Mathf.CeilToInt(_renderTexture.height / (float)BlockSize / yGroupSize),
            1);
        
        Graphics.Blit(_renderTexture, destination);
    }

    bool DisablePixelation()
    {
        return !isOn;
    }
}
