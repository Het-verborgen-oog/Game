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
    private Slider MinimumSlider;

    [SerializeField]
    private Slider MaximumSlider;

    [Header("Text Boxes")]
    [SerializeField]
    private TextMeshProUGUI ValueText;

    [SerializeField]
    private TextMeshProUGUI MinimumText;

    [SerializeField]
    private TextMeshProUGUI MaximumText;

    [SerializeField]
    private TextMeshProUGUI InstructionText;

    [Header("Input")]
    [SerializeField]
    private ArduinoControls arduino;

    [SerializeField]
    private ArduinoControls.MeasureDataIndex RequestedData;

    [SerializeField]
    private float WaitUntilCapture = 6f;

    private float newMinimum = 0f, newMaximum = 0f;

    // Monobehaviour Methods
    void Update()
    {
        UpdateText();
    }

    private void OnEnable()
    {
        UpdateSliders();
    }

    // Public Methods
    public void SwitchDataType(int data)
    {
        RequestedData = (ArduinoControls.MeasureDataIndex) Enum.ToObject(typeof(ArduinoControls.MeasureDataIndex), data);
    }

    public void CalibrateMaximum()
    {
        StartCoroutine(IngestMaximum());
    }

    public void CalibrateMinimum()
    {
        StartCoroutine(IngestMinimum());
    }

    public void SaveData()
    {
        ArduinoControls.MeasureData oldData = arduino.DataCollection[(int)RequestedData];
        ArduinoControls.MeasureData newData = new ArduinoControls.MeasureData(newMinimum, newMaximum, oldData.OutputMinimum, oldData.OutputMaximum);
        arduino.DataCollection[(int)RequestedData] = newData;
        arduino.Save(RequestedData);
    }

    // Private Methods
    private void UpdateText()
    {
        ValueText.text = arduino.GrabRawProperty(RequestedData).ToString();
    }

    private enum Input_Data
    {
        Minimum = 0,
        Maximum = 1,
    }

    private void UpdateSliders()
    {
        float[] arr = arduino.DataCollection[(int)RequestedData].GetPropertyValues();
        MinimumText.text = arr[(int)Enum.ToObject(typeof(Input_Data), Input_Data.Minimum)].ToString();
        MinimumSlider.value = arr[(int)Enum.ToObject(typeof(Input_Data), Input_Data.Minimum)];
        MaximumText.text = arr[(int)Enum.ToObject(typeof(Input_Data), Input_Data.Maximum)].ToString();
        MaximumSlider.value = arr[(int)Enum.ToObject(typeof(Input_Data), Input_Data.Maximum)];
        
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
        UpdateSliders();
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

        UpdateSliders();
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

    /// <summary>
    /// A function to display instructions or notifications onscreen.
    /// </summary>
    /// <param name="text">The text to display</param>
    private void DisplayInstruction(string text)
    {
        InstructionText.text = text;
    }
}