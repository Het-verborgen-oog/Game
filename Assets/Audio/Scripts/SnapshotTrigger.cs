using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// A GameObject that functions as a Snapshot Trigger always requires a Collider
[RequireComponent(typeof(Collider))]
public class SnapshotTrigger : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";

    [Header("Snapshots")]
    [SerializeField]
    [Tooltip("What snapshot to transition into when entering the trigger. If left emtpy, no entering-snapshot will be applied")]
    private AudioMixerSnapshot enterSnapshot;

    [SerializeField]
    [Tooltip("What snapshot to transition into when leaving the trigger. If left emtpy, no exit-snapshot will be applied")]
    private AudioMixerSnapshot exitSnapshot;

    
    [Header("Transition")]
    [SerializeField]
    [Tooltip("The fade-in for the entering-snapshot")]
    private float fadeInDuration = 2f;
    
    [SerializeField]
    [Tooltip("The fade-out for the exit-snapshot")]
    private float fadeOutDuration = 2f;

    
    // MonoBehaviour Methods
    private void OnTriggerEnter(Collider collider) {
        Debug.Log("Enter: " + collider.name);
        if (!collider.CompareTag(playerTag)) { return; }
        if (enterSnapshot != null)
        {
            enterSnapshot.TransitionTo(fadeInDuration);
        }
    }

    private void OnTriggerExit(Collider collider) {
        Debug.Log("Exit: " + collider.name);
        if (!collider.CompareTag(playerTag)) { return; }
        if (exitSnapshot != null)
        {
            exitSnapshot.TransitionTo(fadeOutDuration);    
        }
    }
}