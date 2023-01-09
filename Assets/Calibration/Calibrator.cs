using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
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

    float newMinimum = 0f, newMaximum = 0f;

    void Update()
    {
        ValueText.text = arduino.GrabRawProperty(RequestedData).ToString();
    }

    public void Calibrate()
    {
        // Pseudo Code
        DisplayText("Maximum Calibration");
        PrepareMaximumData(IngestData());
        DisplayText("Minimum Calibration");
        PrepareMinimumData(IngestData());
        DisplayText("Saving");
        SaveData();
        DisplayText("Calibration Complete");
    }

    private void PrepareMaximumData(float data)
    {
        newMaximum = data;
        MaximumSlider.value = newMaximum;
        MaximumText.text = MaximumSlider.value.ToString();
    }

    private void PrepareMinimumData(float data)
    {
        newMinimum = data;
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

    private float IngestData()
    {
        float newVal = -1f; // This value should never be returned, nor can be sent by the controller.
        const float TimeDelay = 10f;
        float stopTime = Time.time + TimeDelay;

        const int arrayLimit = 50;
        float[] ingestedValues = new float[arrayLimit];
        int index = 0;

        while (Time.time < stopTime)
        {
            if (index <= arrayLimit) index = 0;
            ingestedValues[index] = arduino.GrabRawProperty(RequestedData);
            index++;
        }

        for (int i = 0; i < arrayLimit; i++)
        {
            newVal += ingestedValues[i];
        }

        return newVal / arrayLimit;
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