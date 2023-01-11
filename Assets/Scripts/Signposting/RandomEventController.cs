using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomEventController : MonoBehaviour
{
    private List<IResponse> randomEventResponses;

    // Monobehaviour Methods
    void Start()
    {
        randomEventResponses = GetComponentsInChildren<IResponse>().ToList();
    }

    private void OnEnable()
    {
        ResetTrigger.OnResetRandomEvents += ResetResponses;
    }

    private void OnDisable()
    {
        ResetTrigger.OnResetRandomEvents -= ResetResponses;
    }

    // Public Methods
    public void Trigger()
    {
        foreach (IResponse response in randomEventResponses)
        {
            response.Activate();
        }
    }

    public void ResetResponses()
    {
        foreach (IResponse response in randomEventResponses)
        {
            response.ResetResponse();
        }
    }
}
