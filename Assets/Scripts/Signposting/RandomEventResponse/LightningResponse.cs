using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightningResponse : MonoBehaviour, IResponse
{
    [Header("Requested Light Settings")]
    [SerializeField]
    private float requestedLightIntensity;

    [SerializeField]
    private Color requestedLightColor;
    
    [SerializeField]
    private float timeToChangeLight;

    private Light lightsource;
    private float startLightIntensity;
    private Color startLightColor;
    
    // Monobehaviour Methods
    void Start()
    {
        startLightIntensity = lightsource.intensity;
        startLightColor = lightsource.color;
    }

    // Public Methods
    public void Activate()
    {
        ChangeLightSource(requestedLightIntensity, requestedLightColor);
    }

    public void ResetResponse()
    {
        ChangeLightSource(startLightIntensity, startLightColor);
    }

    public void ChangeLightSource(float lightIntensity, Color lightColor)
    {
        lightsource.color = Color.Lerp(lightsource.color, lightColor, timeToChangeLight);
        lightsource.intensity = Mathf.Lerp(lightsource.intensity, lightIntensity, timeToChangeLight);
    }
}
