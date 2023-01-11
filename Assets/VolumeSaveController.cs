using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSaveController : MonoBehaviour
{
    [SerializeField] 
    private Slider volumeSlider = null;

    [SerializeField] 
    private Text VolumeTextUI = null;

    // Monobehaviour Methods
    private void Start()
    {
        LoadValues();
    }

    // Public Methods
    public void VolumeSlider(float volume)
    {
        VolumeTextUI.text = volume.ToString("0.0");
    }

    public void SaveVolumeButton()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    // Private Methods
    private void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}