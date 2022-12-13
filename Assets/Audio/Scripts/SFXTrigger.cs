using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A GameObject that functions as an SFX Trigger always requires a Collider
[RequireComponent(typeof(Collider))]
public class SFXTrigger : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";

    [Header("SFX")]
    [SerializeField]
    [Tooltip("Audio source to play the SFX over")]
    private AudioSource audioSource;

    [SerializeField]
    [Tooltip("SFX to play on entering the trigger. If left empty, no entering-SFX will be played")]
    private SFXCollection enterSFX = new SFXCollection {minPitch = 0.5f, maxPitch = 1.5f };

    [SerializeField]
    [Tooltip("SFX to play on exiting the trigger. If left empty, no exit-SFX will be played")]
    private SFXCollection exitSFX = new SFXCollection {minPitch = 0.5f, maxPitch = 1.5f };


    // MonoBehaviour Methods
    private void OnTriggerEnter(Collider collider) {
        Debug.Log(collider.name);
        if (!collider.CompareTag(playerTag)) { return; }
        if (enterSFX.audioClips.Length > 0 && audioSource != null)
        {
            PlaySFX(enterSFX);
        }
    }
    
    private void OnTriggerExit(Collider collider) {
        if (!collider.CompareTag(playerTag)) { return; }
        if (exitSFX.audioClips.Length > 0 && audioSource != null)
        {
            PlaySFX(exitSFX);
        }
    }


    // Private Methods
    private void PlaySFX(SFXCollection playSFX)
    {
        if (playSFX.audioClips.Length == 0) { return; }
        audioSource.pitch = Random.Range(playSFX.minPitch, playSFX.maxPitch);

        int selectedSound = Random.Range(1, playSFX.audioClips.Length) * System.Convert.ToInt32(playSFX.audioClips.Length > 1);
        audioSource.clip = playSFX.audioClips[selectedSound];

        playSFX.audioClips[selectedSound] = playSFX.audioClips[0];
        playSFX.audioClips[0] = audioSource.clip;
        audioSource.Play();
    }
}
