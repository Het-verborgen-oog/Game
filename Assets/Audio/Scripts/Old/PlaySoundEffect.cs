using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEffect : MonoBehaviour
{
    public AudioSource soundPlayer;

    // Public Methods
    public void playSoundEffect()
    {
        soundPlayer.Play();
    }
}
