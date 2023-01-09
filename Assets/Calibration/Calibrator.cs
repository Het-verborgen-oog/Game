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
    TextMeshProUGUI InstructionText;

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

    public void Calibrate()
    {
        // Pseudo Code
        //DisplayText("Maximum Calibration");
        MaximumCalibration();
        //DisplayText("Minimum Calibration)";
        MinimumCalibration();
        //DisplayText("Saving");
        SaveData();
        //DisplayText("Calibration Complete");
    }

    private void MaximumCalibration()
    {
        newMaximum = arduino.GrabRawProperty(RequestedData);
        MaximumSlider.value = newMaximum;
        MaximumText.text = MaximumSlider.value.ToString();
    }

    private void MinimumCalibration()
    {
        newMinimum = arduino.GrabRawProperty(RequestedData);
        MinimumSlider.value = newMinimum;
        MinimumText.text = MinimumSlider.value.ToString();
    }

    private void SaveData()
    {
        ArduinoControls.MeasureData oldData = arduino.DataCollection[(int)RequestedData];
        ArduinoControls.MeasureData newData = new ArduinoControls.MeasureData(newMinimum, newMaximum, oldData.OutputMinimum, oldData.OutputMaximum);
        arduino.DataCollection[(int)RequestedData] = newData;
        arduino.Save(RequestedData);
    }

    /// <summary>
    /// A function to display instructions or notifications onscreen.
    /// </summary>
    /// <param name="text">The text to display</param>
    private void DisplayText(string text)
    {
        InstructionText.text = text;
    }
}