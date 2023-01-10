using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalPlayerGuider : PlayerGuider
{
    [SerializeField]
    private TrackSide requiredDirectionHorizontal, requiredDirectionVertical;

    public override void ToggleParticleSystem(TrackSide playerDirectionHorizontal, TrackSide playerDirectionVertical)
    {
        if (playerDirectionVertical == requiredDirectionVertical && playerDirectionHorizontal == requiredDirectionHorizontal)
        {
            visualGuidance.ShowPath();
        }
        else
        {
            visualGuidance.HidePath();
        }
    }
}
