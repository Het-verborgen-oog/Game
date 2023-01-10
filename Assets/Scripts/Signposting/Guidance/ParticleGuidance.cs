using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleGuidance : MonoBehaviour, IVisualGuidance
{
    [SerializeField]
    protected ParticleSystem particleSystem;
    // Start is called before the first frame update
    protected void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }
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
