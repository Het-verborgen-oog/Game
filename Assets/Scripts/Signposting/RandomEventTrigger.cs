using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RandomEventTrigger : MonoBehaviour, IInteractable
{
    [SerializeField]
    [Range(0, 100)]
    private int activationPercentage;

    const string PLAYERTAG = "Player";
    private RandomEventController randomEventController;
    private void Start()
    {
        randomEventController = GetComponentInParent<RandomEventController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(PLAYERTAG))
        {
            CheckForChance();
        }
    }

    private void CheckForChance()
    {
        int randomNumber = Random.Range(0, 100);
        if (randomNumber > activationPercentage) return;
        Trigger();
    }

    public void Trigger()
    {
        randomEventController?.Trigger();
    }
}
