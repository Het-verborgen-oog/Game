using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ResetTrigger : MonoBehaviour
{
    public delegate void ResetRandomEvents();
    public static event ResetRandomEvents OnResetRandomEvents;

    const string PLAYERTAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYERTAG))
        {
            OnResetRandomEvents?.Invoke();
        }
    }
}
