using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlayerGuider : PlayerGuider
{
    [SerializeField]
    private TrackSide requiredDirectionHorizontal;
    private TrackSide savedDirection;

    public override void ToggleParticleSystem(TrackSide playerDirectionHorizontal, TrackSide playerDirectionVertical)
    {
        if (playerDirectionHorizontal == requiredDirectionHorizontal)
        {
            if (savedDirection == playerDirectionHorizontal ) return;
            visualGuidance.ShowPath();
        }
        else
        {
            if (savedDirection != playerDirectionHorizontal) return;
            visualGuidance.HidePath();
        }
        savedDirection = playerDirectionHorizontal;
    }
}
