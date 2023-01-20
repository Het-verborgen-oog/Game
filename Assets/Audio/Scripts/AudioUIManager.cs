using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioUIManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject mainButtons;
    [SerializeField] 
    private GameObject audioButtons;

    [SerializeField] 
    private AudioMixer audioMixer;

    [SerializeField] 
    private Slider effectSlider;

    [SerializeField] 
    private Slider backgroundSlider;

    // Public Methods
    public void LoadAudioMenu()
    {
        mainButtons.SetActive(false);
        audioButtons.SetActive(true);
    }
    public void HideAudioMenu()
    {
        audioButtons.SetActive(false);
        mainButtons.SetActive(true);
    }

    public void SaveSound()
    {
        audioMixer.SetFloat("MasterVolume", effectSlider.value);
    }
}
