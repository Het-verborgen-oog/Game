using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayer : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField]
    [Tooltip("Audio source to play the SFX over.")]
    private AudioSource audioSource;

    [SerializeField]
    [Tooltip("List of soundeffects that will be played at random.")]
    private SFXCollection soundEffects = new SFXCollection {MinPitch = 0.5f, MaxPitch = 1.5f };

    // Public Methods
    public void Play()
    {
        if (soundEffects.audioClips.Length == 0) { return; }
        audioSource.pitch = Random.Range(soundEffects.MinPitch, soundEffects.MaxPitch);

        int selectedSound = Random.Range(1, soundEffects.audioClips.Length) * System.Convert.ToInt32(soundEffects.audioClips.Length > 1);
        audioSource.clip = soundEffects.audioClips[selectedSound];

        soundEffects.audioClips[selectedSound] = soundEffects.audioClips[0];
        soundEffects.audioClips[0] = audioSource.clip;
        audioSource.Play();
    }
}
