using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalPlayerGuider : PlayerGuider
{
    [SerializeField]
    private TrackSide requiredDirectionHorizontal, requiredDirectionVertical;

    private TrackSide savedDirectionH, savedDirectionV;

    // Public Methods
    public override void ToggleParticleSystem(TrackSide playerDirectionHorizontal, TrackSide playerDirectionVertical)
    {
        if (playerDirectionVertical == requiredDirectionVertical && playerDirectionHorizontal == requiredDirectionHorizontal)
        {
            if (savedDirectionV == playerDirectionVertical && savedDirectionH == playerDirectionHorizontal) return;
            visualGuidance.ShowPath();
        }
        else
        {
            if (savedDirectionV != playerDirectionVertical || savedDirectionH != playerDirectionHorizontal) return;
            visualGuidance.HidePath();
        }
        savedDirectionV = playerDirectionVertical;
        savedDirectionH = playerDirectionHorizontal;
    }
}
