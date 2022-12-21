using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrollbar : MonoBehaviour
{
    [SerializeField]
    ArduinoControls arduinoControl;

    [SerializeField]
    float VerticalTranslateSpeed = 1;

    [SerializeField]
    float RotationSpeed = 1;

    const float NoMovement = 0;
    const float MaximumOffset = (1200 - 400) / 2; //(Image size - RectTransformSize) / 2

    RectTransform rectTransform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (arduinoControl == null) return;
    }

    private void Update()
    {
        if (arduinoControl.isConnected() == false) return;
        HandleVerticalInput(Input.GetAxisRaw("Vertical"));
        HandleHorizontalInput(Input.GetAxisRaw("Horizontal"));
    }

    void HandleVerticalInput(float input)
    {
        if (input == NoMovement) return;
        rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + (input * VerticalTranslateSpeed), rectTransform.position.z);
    }

    void HandleHorizontalInput(float input)
    {
        if (input == NoMovement) return;
        rectTransform.Rotate(0, 0, rectTransform.rotation.z + -(RotationSpeed * input));
    }
}
