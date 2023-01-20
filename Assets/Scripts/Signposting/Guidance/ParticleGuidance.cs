using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleGuidance : MonoBehaviour, IVisualGuidance
{
    [SerializeField]
    protected ParticleSystem particleSystem;
    
    // Monobehaviour Methods
    protected void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Public Methods
    public virtual void HidePath()
    {
        particleSystem.Clear();
        particleSystem.Stop();
    }

    public virtual void ShowPath()
    {
        if (particleSystem.isPlaying) return;
        particleSystem.Play();
    }
}
