using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplitPath : MonoBehaviour
{
    [SerializeField]
    private List<PlayerGuider> playerGuiders;
    // Start is called before the first frame update
    void Start()
    {
        playerGuiders = GetComponentsInChildren<PlayerGuider>().ToList();
    }

    public void ToggleGodrayGuiders(TrackSide playerDirectionHorizontal, TrackSide playerDirectionVertical)
    {
        foreach (PlayerGuider godrayGuider in playerGuiders)
        {
            godrayGuider.ToggleParticleSystem(playerDirectionHorizontal, playerDirectionVertical);
        }
    }

}
