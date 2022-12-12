using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplitPath : MonoBehaviour
{
    [SerializeField]
    private List<GodrayGuider> godrayGuiders;
    // Start is called before the first frame update
    void Start()
    {
        godrayGuiders = GetComponentsInChildren<GodrayGuider>().ToList();
    }

    public void ToggleGodrayGuiders(TrackSide playerDirectionHorizontal, TrackSide playerDirectionVertical)
    {
        foreach (GodrayGuider godrayGuider in godrayGuiders)
        {
            godrayGuider.ToggleParticleSystem(playerDirectionHorizontal, playerDirectionVertical);
        }
    }

}
