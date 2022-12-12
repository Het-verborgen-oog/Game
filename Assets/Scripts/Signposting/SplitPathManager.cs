using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplitPathManager : MonoBehaviour
{
    [SerializeField]
    private List<SplitPath> splitpathes;

    private TrackSide playerDirectionHorizontal, playerDirectionVertical;
    private void OnEnable()
    {
        DolphinMovement.OnPlayerMoved += ToggleSplitPath;
    }

    // Start is called before the first frame update
    void Start()
    {
        splitpathes = GetComponentsInChildren<SplitPath>().ToList();
    }

    private void ToggleSplitPath(TrackSide horizontal, TrackSide vertical)
    {
        foreach (SplitPath splitPath in splitpathes)
        {
            splitPath.ToggleGodrayGuiders(horizontal, vertical);
        }
    }

    private void OnDisable()
    {
        DolphinMovement.OnPlayerMoved -= ToggleSplitPath;
    }
}
