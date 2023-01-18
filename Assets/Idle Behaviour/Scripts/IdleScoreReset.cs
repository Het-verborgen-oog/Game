using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdleScoreReset : MonoBehaviour, IIdleAction
{
    public void SwitchIdleState(bool isIdle)
    {
        if (isIdle)
        {
            Debug.Log("reset");
            ScoreManager.Reset();
        }
    }
}
