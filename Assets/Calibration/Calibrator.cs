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

    [SerializeField]
    float WaitUntilCapture = 6f;

    float newMinimum = 0f, newMaximum = 0f;

    private bool isCalibrating = false, isIdle = true;
    private LastCalibrationState state = LastCalibrationState.None;

    enum LastCalibrationState
    {
        Minimum = 0,
        Maximum = 1,
        None = 2
    }

    void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        ValueText.text = arduino.GrabRawProperty(RequestedData).ToString();
    }
    public void CalibrateMaximum()
    {
        StartCoroutine(IngestMaximum());
    }

    public void CalibrateMinimum()
    {
        StartCoroutine(IngestMinimum());
    }

    private IEnumerator IngestMaximum()
    {
        float result = -1;
        const int arrayLimit = 20;

        float[] ingestedValues = new float[arrayLimit];

        for (int i = (int)WaitUntilCapture; i > 0; i--)
        {
            DisplayInstruction("Measuring in: " + i);
            yield return new WaitForSeconds(1);
        }

        DisplayInstruction("Measuring");

        for (int i = 0; i < arrayLimit; i++)
        {
            ingestedValues[i] = arduino.GrabRawProperty(RequestedData);

            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < arrayLimit; i++)
        {
            result += ingestedValues[i];
        }

        result /= arrayLimit;

        DisplayInstruction("Calibration Complete");

        PrepareMaximumData(result);
        yield return null;
    }

    private IEnumerator IngestMinimum()
    {
        float result = -1;
        const int arrayLimit = 20;

        float[] ingestedValues = new float[arrayLimit];

        for (int i = (int)WaitUntilCapture; i > 0; i--)
        {
            DisplayInstruction("Measuring in: " + i);
            yield return new WaitForSeconds(1);
        }

        DisplayInstruction("Measuring");

        for (int i = 0; i < arrayLimit; i++)
        {
            ingestedValues[i] = arduino.GrabRawProperty(RequestedData);

            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < arrayLimit; i++)
        {
            result += ingestedValues[i];
        }

        result /= arrayLimit;

        DisplayInstruction("Calibration Complete");

        PrepareMinimumData(result);
        yield return null;
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

    public void SaveData()
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
    private void DisplayInstruction(string text)
    {
        InstructionText.text = text;
        Debug.Log(text);
    }

    public void SwitchDataType(int data)
    {
        RequestedData = (ArduinoControls.MeasureDataIndex) Enum.ToObject(typeof(ArduinoControls.MeasureDataIndex), data);
    }
}