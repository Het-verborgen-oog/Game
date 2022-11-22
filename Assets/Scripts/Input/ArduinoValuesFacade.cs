using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoValuesFacade : MonoBehaviour
{
    private ArduinoControls arduinoLibraryHandler;

    private void Start()
    {
        arduinoLibraryHandler = GetComponent<ArduinoControls>();
    }

    public bool CheckIfConnected()
    {
        return arduinoLibraryHandler.isConnected();
    }
    public float GetSpeed()
    {
        return arduinoLibraryHandler.Speed;
    }
    public float GetDirection()
    {
        Debug.Log(arduinoLibraryHandler.Roll);
        return arduinoLibraryHandler.Roll;
    }
    public float GetHeight()
    {
        return arduinoLibraryHandler.Pitch;
    }
}
