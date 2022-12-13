using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(IArduinoData))]
public class IdleBehaviour : MonoBehaviour
{
    [SerializeField]
    public float SamplesPerSecond = 5f;

    [SerializeField]
    public float SampleCount = 25f;

    [SerializeField]
    public float SecondsTillIdle = 30f;

    [SerializeField]
    [Range(0f, 100f)]
    [Tooltip("Percentile allowed deviation")]
    public float AllowedDeviation = 5f;

    [SerializeField]
    private List<float> pitchMeasurements;

    [SerializeField]
    private List<float> rollMeasurements;

    private ArduinoControls arduinoControls;

    [SerializeField]
    private bool stationairy;
    private float timeSinceStationairy;

    public bool IsIdle {
        get {
            return stationairy && (Time.timeSinceLevelLoad > timeSinceStationairy + SecondsTillIdle);
        }
    }

    private List<IIdleAction> idleActions;


    private void OnEnable() {
        pitchMeasurements = new List<float>();
        rollMeasurements = new List<float>();

        arduinoControls = GetComponent<ArduinoControls>();
        idleActions = new(FindObjectsOfType<MonoBehaviour>().OfType<IIdleAction>());
        

        StartCoroutine(GetMeasurments());
        StartCoroutine(CallIdleActions());
    }

    private void Update() {
        if (!arduinoControls.isConnected())
        {
            return;
        }

        float pitch = arduinoControls.Pitch;
        float roll = arduinoControls.Roll;

        if (
            IsWithinRange(pitch, GetAverage(pitchMeasurements), AllowedDeviation) &&
            IsWithinRange(roll, GetAverage(rollMeasurements), AllowedDeviation)
        ) 
        {
            // Controller is not moving
            if (!stationairy) 
            {
                stationairy = true;
                timeSinceStationairy = Time.timeSinceLevelLoad;
            }
        } else {
            // Controller moved
            stationairy = false;
        }
    }


    // Private Methods
    private IEnumerator GetMeasurments()
    {
        while(true)
        {
            if (!arduinoControls.isConnected())
            {
                yield return new WaitForSeconds(1 / SamplesPerSecond);
                continue;
            }

            if (pitchMeasurements.Count == SampleCount)
            {
                pitchMeasurements.RemoveAt(0);
                rollMeasurements.RemoveAt(0);
            }

            pitchMeasurements.Add(arduinoControls.Pitch);
            rollMeasurements.Add(arduinoControls.Roll);

            yield return new WaitForSeconds(1 / SamplesPerSecond);
        }
    }

    private IEnumerator CallIdleActions()
    {
        while(true)
        {
            foreach(IIdleAction idleAction in idleActions)
            {
                idleAction.SwitchIdleState(IsIdle);
            }
            yield return new WaitForSeconds(1);
        }
    }

    private float GetAverage(List<float> values)
    {
        float total = 0;

        foreach(float value in values)
        {
            total += value;
        } 

        return (total / values.Count); 
    }

    private bool IsWithinRange(float value, float comparedValue, float allowedDeviation)
    {
        return value >= (comparedValue - (allowedDeviation / 100)) && value <= comparedValue + (allowedDeviation / 100);
    }
}
