using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignpostTrigger : MonoBehaviour, IInteractable
{
    const string PLAYERTAG = "Player";
    private IResponse signpostResponse; 
    // Start is called before the first frame update
    void Start()
    {
        signpostResponse = GetComponentInChildren<IResponse>();
    }

    public void Trigger()
    {
        signpostResponse.Activate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(PLAYERTAG))
        {
            Trigger();
        }
    }
}
