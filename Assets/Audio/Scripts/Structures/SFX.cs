using UnityEngine;

[System.Serializable]
public struct SFXCollection {
    [SerializeField]
    public AudioClip[] audioClips;

    [SerializeField]
    public float MinPitch;

    [SerializeField]
    public float MaxPitch;
 }