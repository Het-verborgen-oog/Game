using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightningResponse : MonoBehaviour, IResponse
{
    private Light lightsource;

    private float startLightIntensity;
    private Color startLightColor;

    [Header("Requested Light Settings")]
    [SerializeField]
    private float requestedLightIntensity;
    [SerializeField]
    private Color requestedLightColor;
    [SerializeField]
    private float timeToChangeLight;
    // Start is called before the first frame update
    void Start()
    {
        startLightIntensity = lightsource.intensity;
        startLightColor = lightsource.color;
    }

    public void Activate()
    {
        ChangeLightSource(requestedLightIntensity, requestedLightColor);
    }

    public void Reset()
    {
        ChangeLightSource(startLightIntensity, startLightColor);
    }

    public void ChangeLightSource(float lightIntensity, Color lightColor)
    {
        lightsource.color = Color.Lerp(lightsource.color, lightColor, timeToChangeLight);
        lightsource.intensity = Mathf.Lerp(lightsource.intensity, lightIntensity, timeToChangeLight);
    }
}
