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
    GameObject MinInputText;

    [SerializeField]
    GameObject MaxInputText;

    [SerializeField]
    GameObject DoneText;

    [Header("Input")]

    [SerializeField]
    ArduinoControls arduino;

    [SerializeField]
    ArduinoControls.MeasureDataIndex RequestedData;

    private enum MeasureState
    { 
        Idle = 0,
        Minimum = 1,
        Maximum = 2,
    }

    MeasureState CurrentState = MeasureState.Idle;
    float newMinimum = 0f, newMaximum = 0f;

    void Update()
    {
        ValueText.text = arduino.GrabRawProperty(RequestedData).ToString();
    }

    public void MaximumCalibration()
    {
        newMaximum = arduino.GrabRawProperty(RequestedData);
        MaximumSlider.value = newMaximum;
        MaximumText.text = MaximumSlider.value.ToString();
    }

    public void MinimumCalibration()
    {
        newMinimum = arduino.GrabRawProperty(RequestedData);
        MinimumSlider.value = newMinimum;
        MinimumText.text = MinimumSlider.value.ToString();
    }

    public void SaveData()
    {
        ArduinoControls.MeasureData oldData = arduino.DataCollection[(int)RequestedData];
        ArduinoControls.MeasureData newData = new ArduinoControls.MeasureData(newMinimum, newMaximum, oldData.OutputMinimum, oldData.OutputMaximum);
        arduino.DataCollection[(int)RequestedData] = newData;
        arduino.Save(RequestedData);
    }
}