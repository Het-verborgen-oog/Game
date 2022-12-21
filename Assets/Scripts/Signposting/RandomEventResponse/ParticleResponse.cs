using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleResponse : MonoBehaviour, IResponse
{
    private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }

    public void Activate()
    {
        particles.Play();
    }

    public void Reset()
    {
        particles.Stop();
    }
}
