using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbiancePlayer : MonoBehaviour
{
    
    [Header("SFX")]
    [SerializeField]
    [Tooltip("Audio source to play the SFX over.")]
    private AudioSource audioSource;

    [SerializeField]
    [Tooltip("List of soundeffects that will be played at random.")]
    private SFXCollection soundEffects = new SFXCollection {MinPitch = 0.5f, MaxPitch = 1.5f };

    [SerializeField]
    [Tooltip("Minimum interval between soundeffect plays in seconds")]
    private float minInterval = 2f;

    [SerializeField]
    [Tooltip("Maximum interval between soundeffect plays in seconds")]
    private float maxInterval = 10f;


    // Monobehaviour Methods
    private void OnEnable() {
        StartPlayer();
    }

    private void OnDisable() {
        StopPlayer();
    }


    // Public Methods
    public void StopPlayer()
    {
        StopCoroutine(PlayRandomSFX());
    }

    public void StartPlayer()
    {
        StopCoroutine(PlayRandomSFX());
        StartCoroutine(PlayRandomSFX());
    }


    // Private Methods
    private IEnumerator PlayRandomSFX()
    {
        while (true) 
        {
            if (soundEffects.audioClips.Length == 0) { yield return new WaitForEndOfFrame(); }

            // Select and Play sound
            audioSource.pitch = Random.Range(soundEffects.MinPitch, soundEffects.MaxPitch);

            int selectedSound = Random.Range(1, soundEffects.audioClips.Length) * System.Convert.ToInt32(soundEffects.audioClips.Length > 1);
            audioSource.clip = soundEffects.audioClips[selectedSound];

            soundEffects.audioClips[selectedSound] = soundEffects.audioClips[0];
            soundEffects.audioClips[0] = audioSource.clip;
            audioSource.Play();


            // Determine delay
            float delay = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(delay);
        }
    }
}
