using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RandomEventTrigger : MonoBehaviour, IInteractable
{
    const string PLAYERTAG = "Player";
    private List<IResponse> randomEventResponses; 
    // Start is called before the first frame update
    void Start()
    {
        randomEventResponses = GetComponentsInChildren<IResponse>().ToList();
    }

    public void Trigger()
    {
        foreach (IResponse response in randomEventResponses)
        {
            response.Activate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(PLAYERTAG))
        {
            Trigger();
        }
    }
}
