using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioUIManager : MonoBehaviour
{
    [SerializeField] GameObject mainButtons;
    [SerializeField] GameObject audioButtons;

    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Slider effectSlider;
    [SerializeField] Slider backgroundSlider;

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
