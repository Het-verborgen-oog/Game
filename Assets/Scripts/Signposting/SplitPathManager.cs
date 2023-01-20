using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplitPathManager : MonoBehaviour
{
    [SerializeField]
    private List<SplitPath> splitpathes;

    [SerializeField]
    private SplitPath playerEnteredSplitPath;

    // Monobehaviour Methods
    void Start()
    {
        splitpathes = GetComponentsInChildren<SplitPath>().ToList();
    }

    private void OnEnable()
    {
        DolphinMovement.OnPlayerMoved += ToggleSplitPath;
        SplitPathTrigger.PlayerEnteredSplitPath += SetEnteredSplitPath;
    }
    
    // Private Methods
    private void SetEnteredSplitPath(SplitPath enteredSplitPath)
    {
        playerEnteredSplitPath = enteredSplitPath;
    }

    private void ToggleSplitPath(TrackSide horizontal, TrackSide vertical)
    {
       if (playerEnteredSplitPath == null) return;
       playerEnteredSplitPath.ToggleGodrayGuiders(horizontal, vertical);
    }

    private void OnDisable()
    {
        DolphinMovement.OnPlayerMoved -= ToggleSplitPath;
        SplitPathTrigger.PlayerEnteredSplitPath -= SetEnteredSplitPath;
    }
}
