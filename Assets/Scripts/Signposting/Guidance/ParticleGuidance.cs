using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleGuidance : MonoBehaviour, IVisualGuidance
{
    [SerializeField]
    protected ParticleSystem particleSystem;
    [SerializeField]
    private float hiddenLifetimeMultiplier = 0.1f;
    private float currentLifetimeMultiplier = 1f;

    ParticleSystem.MainModule main;

    // Monobehaviour Methods
    protected void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        main = particleSystem.main;
    }

    // Public Methods
    public virtual void HidePath()
    {
        particleSystem.Clear();
        particleSystem.Stop();
    }

    public virtual void PausePath()
    {
        if (particleSystem.isPlaying) return;
            particleSystem.Stop();

    }

    public virtual void ShowPath()
    {
        if (particleSystem.isPlaying) return;
        particleSystem.Play();
    }
}
