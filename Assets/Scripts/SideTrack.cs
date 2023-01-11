using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class SideTrack : MonoBehaviour
{
    [SerializeField] public CinemachinePath track;
    [SerializeField] public TrackSide trackSide;
    [SerializeField] public bool unlocked = true;
    [SerializeField] public float transferToPos;
    [SerializeField] public float transferBackPos;
    [SerializeField] public CinemachinePath trackToSwitchFrom;
    [SerializeField] public CinemachinePath trackToSwitchBackTo;
}

// Support Structures
public enum TrackSide
{
    neutral,
    left,
    right,
    up,
    down
}