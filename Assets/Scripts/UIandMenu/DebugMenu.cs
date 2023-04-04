using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    //[SerializeField] bool debugMode = false;
    [Header("Post Processing", order = 1)]
    [SerializeField] Toggle ppToggle;
    [Header("Bloom", order = 2)]
    [SerializeField] BCBloom bloom;

    [SerializeField] Toggle bloomToggle;
    [SerializeField][Range(1, 16)] int bloomIterations = 2;
    [SerializeField][Range(0, 10)] float bloomThreshold = 2.1f;
    [SerializeField][Range(0, 10)] float bloomIntensity = 1.4f;

    [SerializeField] Slider bloomIterationsSlider;
    [SerializeField] Slider bloomThresholdSlider;
    [SerializeField] Slider bloomIntensitySlider;

    [Header("Dof", order = 2)]
    
    [SerializeField] DepthOfField blur;
    [SerializeField] Toggle dofToggle;

    [SerializeField] int blurDepth;
    [SerializeField] int blurRange;

    [SerializeField] Slider blurDepthSlider;
    [SerializeField] Slider blureRangeSlider;

    [Header("Pixelization", order = 2)]
    [SerializeField] Pixelization pixelization;
    [SerializeField] Toggle pixelToggle;
    [SerializeField] int pixelationAmount;

    [SerializeField] [Range(1,32)]int pixelAmount;

    [SerializeField] Slider pixelAmountSlider;

    [Header("LUT", order = 2)]
    [SerializeField] CameraLUTRenderer lutCamera;
    [SerializeField] Toggle LUTToggle;
    [SerializeField] Slider LUTSlider;
    [SerializeField] List<Material> LutMats = new List<Material>(); 

    [Header("Material", order = 1)]
    [SerializeField] List<MaterialSwap> mathandler = new List<MaterialSwap>();

    [SerializeField] int materialSetting = 11;
    [SerializeField] Slider materialSettingSlider;

    void Awake()
    {
        bloomIterationsSlider.maxValue = 16;
        bloomIterationsSlider.value = bloom.iterations;
        bloomThresholdSlider.maxValue = 10;
        bloomThresholdSlider.value = bloom.threshold;
        bloomIntensitySlider.maxValue = 10;
        bloomIntensitySlider.value = bloom.intensity;

        blurDepthSlider.maxValue = 10;
        blurDepthSlider.value = blur.focusDistance;
        blureRangeSlider.maxValue = 10;
        blureRangeSlider.value = blur.focusRange;

        pixelAmountSlider.maxValue = 32;
        pixelAmountSlider.minValue = 1;
        pixelAmountSlider.value = 4;

        LUTSlider.maxValue = LutMats.Count-1;

        //materialSettingSlider.maxValue = materialSetting-1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleBloom()
    {
        bloom.enabled = bloomToggle.isOn;
    }

    public void ChangeBloom()
    {
        bloom.intensity = bloomIntensitySlider.value;
        bloom.threshold = bloomThresholdSlider.value;
        bloom.iterations = (int)bloomIterationsSlider.value;
    }

    public void ToggleDoF()
    {
        blur.enabled = dofToggle.isOn;
    }

    public void ChangeDoF()
    {
        blur.focusRange = blureRangeSlider.value;
        blur.bokehRadius = blurDepthSlider.value;
    }

    public void TogglePixelization()
    {
        pixelization.enabled = pixelToggle.isOn;
    }

    public void ChangePixels()
    {
        pixelization.BlockSize = (int)pixelAmountSlider.value;
    }

    public void ToggleLUT()
    {
        lutCamera.enabled = LUTToggle.isOn;
    }

    public void ChangeLut()
    {
        lutCamera.LUTMaterial = LutMats[(int)LUTSlider.value];
    }


    public void ToggleMaterial()
    {
       foreach (MaterialSwap mat in mathandler)
        {
            mat.ChangeMaterial((int)materialSettingSlider.value);
        }
    }


}
