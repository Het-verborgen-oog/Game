using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calibrator : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField]
    Slider MinimumSlider;

    [SerializeField]
    Slider MaximumSlider;

    [Header("Text Boxes")]
    [SerializeField]
    TextMeshProUGUI ValueText;

    [SerializeField]
    TextMeshProUGUI MinimumText;

    [SerializeField]
    TextMeshProUGUI MaximumText;

    [SerializeField]
    ArduinoControls arduino;

    [SerializeField]
    ArduinoControls.MeasureDataIndex RequestedData;

    private enum MeasureState
    { 
        Idle = 0,
        Minimum = 1,
        Maximum = 2
    }

    MeasureState CurrentState = MeasureState.Idle;
    float lastArduinoValue;
    bool lastInputConfirmed;

    private void Start()
    {
    }

    void Update()
    {
        if (CurrentState != MeasureState.Idle)
        {
            MinimumSlider.value = arduino.DataCollection[(int)RequestedData].InputMinimum;
            MinimumText.text = MinimumSlider.value.ToString();

            MaximumSlider.value = arduino.DataCollection[(int)RequestedData].InputMaximum;
            MaximumText.text = MaximumSlider.value.ToString();
        }
        
        ValueText.text = arduino.GrabRawProperty(RequestedData).ToString();
    }

    public void Calibrate()
    {
        switch (CurrentState)
        {
            case MeasureState.Idle:
                MinimumCalibration();
                CurrentState = MeasureState.Minimum;
                break;
            case MeasureState.Minimum:
                MaximumCalibration();
                CurrentState = MeasureState.Maximum;
                break;
            case MeasureState.Maximum:
                SaveData();
                CurrentState = MeasureState.Idle;
                break;
            default:
                break;
        }
    }

    private void MaximumCalibration()
    {
        lastArduinoValue = arduino.GrabRawProperty(RequestedData);
    }

    private void SaveData()
    {
        Debug.Log("Saving!");
    }

    private void MinimumCalibration()
    {
        
    }
}
