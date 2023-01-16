using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlayerGuider : PlayerGuider
{
    [SerializeField]
    private TrackSide requiredDirectionVertical;


    private TrackSide savedDirection;

    // Public Methods
    public override void ToggleParticleSystem(TrackSide playerDirectionHorizontal, TrackSide playerDirectionVertical)
    {
        if (playerDirectionVertical == requiredDirectionVertical)
        {
            if (savedDirection == playerDirectionVertical) return;
            visualGuidance.ShowPath();
        }
        else
        {
            if (savedDirection != playerDirectionVertical) return;
            visualGuidance.HidePath();
        }
        savedDirection = playerDirectionVertical;
    }
}
