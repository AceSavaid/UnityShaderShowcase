using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropDown;
    [SerializeField] Toggle fullScreenToggle;

    void Start()
    {
        if (PlayerPrefs.HasKey("Resolution"))
        {
            resolutionDropDown.value = PlayerPrefs.GetInt("Resolution");
            ChangeResolution();
        }
        if (PlayerPrefs.HasKey("FullScreen"))
        {
            if (PlayerPrefs.GetInt("FullScreen") == 1)
            {
                fullScreenToggle.isOn = true;
            }
            else
            {
                fullScreenToggle.isOn = false;
            }
            ToggleFullScreen();
        }
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = fullScreenToggle.isOn;
        if (fullScreenToggle.isOn)
        {
            PlayerPrefs.SetInt("FullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FullScreen", 0);
        }
    }

    public void ChangeResolution()
    {
        string[] resnames = resolutionDropDown.captionText.text.Split(char.Parse("x"));
        Screen.SetResolution(int.Parse(resnames[0]), int.Parse(resnames[1]), fullScreenToggle.isOn);
        PlayerPrefs.SetInt("Resolution", resolutionDropDown.value);
    }
}
