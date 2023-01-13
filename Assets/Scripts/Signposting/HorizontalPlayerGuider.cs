using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlayerGuider : PlayerGuider
{
    [SerializeField]
    private TrackSide requiredDirectionHorizontal;

    private TrackSide savedDirection;

    // Public Methods
    public override void ToggleParticleSystem(TrackSide playerDirectionHorizontal, TrackSide playerDirectionVertical)
    {
        if (playerDirectionHorizontal == requiredDirectionHorizontal)
        {
            if (savedDirection == playerDirectionHorizontal ) return;
            visualGuidance.ShowPath();
        }
        else if(playerDirectionHorizontal == TrackSide.neutral)
        {
            if (savedDirection == TrackSide.neutral) return;
            visualGuidance.PausePath();
        }

        else
        {
            if (savedDirection != playerDirectionHorizontal) return;
            visualGuidance.HidePath();
        }
        savedDirection = playerDirectionHorizontal;
    }
}
