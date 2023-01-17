using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SplitPath : MonoBehaviour
{
    [SerializeField]
    private List<PlayerGuider> playerGuiders;

    // Monobehaviour Methods
    void Start()
    {
        playerGuiders = GetComponentsInChildren<PlayerGuider>().ToList();
    }

    // Public Methods
    public void ToggleGodrayGuiders(TrackSide playerDirectionHorizontal, TrackSide playerDirectionVertical)
    {
        foreach (PlayerGuider godrayGuider in playerGuiders)
        {
            godrayGuider.ToggleParticleSystem(playerDirectionHorizontal, playerDirectionVertical);
        }
    }
}
