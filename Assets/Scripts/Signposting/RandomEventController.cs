using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomEventController : MonoBehaviour
{
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

    public void ResetResponses()
    {
        foreach (IResponse response in randomEventResponses)
        {
            response.Reset();
        }
    }

}
