using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class GodrayGuider : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem godRayParticleSystem;
    [SerializeField]
    private TrackSide requiredDirectionHorizontal, requiredDirectionVertical;
    // Start is called before the first frame update
    void Start()
    {
        godRayParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void ToggleParticleSystem(TrackSide playerDirectionHorizontal, TrackSide playerDirectionVertical)
    {
        if(playerDirectionHorizontal == requiredDirectionHorizontal && playerDirectionVertical == requiredDirectionVertical)
        {
            if (godRayParticleSystem.isPlaying) return;
            godRayParticleSystem.Play();
        }
        else
        {
            if (godRayParticleSystem.isPlaying)
            {
                godRayParticleSystem.Stop();
            }
        }
    }

}
