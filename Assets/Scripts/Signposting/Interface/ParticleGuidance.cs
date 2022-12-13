using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleGuidance : MonoBehaviour, IVisualGuidance
{
    [SerializeField]
    private ParticleSystem godRayParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        godRayParticleSystem = GetComponentInChildren<ParticleSystem>();
    }
    public void HidePath()
    {
        godRayParticleSystem.Clear();
        godRayParticleSystem.Stop();
    }

    public void ShowPath()
    {
        if (godRayParticleSystem.isPlaying) return;
        godRayParticleSystem.Play();
    }
}
