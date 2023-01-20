using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleResponse : MonoBehaviour, IResponse
{
    private ParticleSystem particles;
    
    // Monobehaviour Methods
    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }

    // Public Methods
    public void Activate()
    {
        particles.Play();
    }

    public void ResetResponse()
    {
        particles.Stop();
    }
}
