using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGuider : MonoBehaviour
{
    protected IVisualGuidance visualGuidance;
    
    // Monobehaviour Methods
    void Start()
    {
        visualGuidance = GetComponentInChildren<IVisualGuidance>();
    }

    // Public Methods
    public abstract void ToggleParticleSystem(TrackSide playerDirectionHorizontal, TrackSide playerDirectionVertical);
}
