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

    void Update()
    {
        MinimumSlider.value = arduino.DataCollection[(int)RequestedData].InputMinimum;
        MinimumText.text = MinimumSlider.value.ToString();

        MaximumSlider.value = arduino.DataCollection[(int)RequestedData].InputMaximum;
        MaximumText.text = MaximumSlider.value.ToString();

    }
}
