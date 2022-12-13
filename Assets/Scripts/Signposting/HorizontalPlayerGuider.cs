using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlayerGuider : PlayerGuider
{
    [SerializeField]
    private TrackSide requiredDirectionHorizontal;

    public override void ToggleParticleSystem(TrackSide playerDirectionHorizontal, TrackSide playerDirectionVertical)
    {
        if (playerDirectionHorizontal == requiredDirectionHorizontal)
        {
            visualGuidance.ShowPath();
        }
        else
        {
            visualGuidance.HidePath();
        }
    }
}
