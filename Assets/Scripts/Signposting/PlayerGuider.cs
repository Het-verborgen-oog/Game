using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGuider : MonoBehaviour
{
    protected IVisualGuidance visualGuidance;
    // Start is called before the first frame update
    void Start()
    {
        visualGuidance = GetComponentInChildren<IVisualGuidance>();
    }

    public abstract void ToggleParticleSystem(TrackSide playerDirectionHorizontal, TrackSide playerDirectionVertical);


}
