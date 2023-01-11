using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTriggerScript : MonoBehaviour
{
    public AreaLightScript areaLightScript;
    public areaNames areaName;

    // Monobehaviour Methods
    private void OnTriggerEnter(Collider other)
    {
        areaLightScript.ChangeLighting(areaName);
    }
}
