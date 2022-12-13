using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlayerGuider : PlayerGuider
{
    [SerializeField]
    private TrackSide requiredDirectionVertical;

    public override void ToggleParticleSystem(TrackSide playerDirectionHorizontal, TrackSide playerDirectionVertical)
    {
        if (playerDirectionVertical == requiredDirectionVertical)
        {
            visualGuidance.ShowPath();
        }
        else
        {
            visualGuidance.HidePath();
        }
    }

}
