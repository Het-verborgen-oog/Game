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

    [SerializeField]
    Slider CenterSlider;

    [Header("Text Boxes")]
    [SerializeField]
    TextMeshProUGUI ValueText;

    [SerializeField]
    TextMeshProUGUI MinimumText;

    [SerializeField]
    TextMeshProUGUI MaximumText;

    [SerializeField]
    TextMeshProUGUI CenterText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
