using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoValuesFacade : MonoBehaviour
{
    private Unity_recive_data_from_Arduino arduinoLibraryHandler;

    private void Start()
    {
        arduinoLibraryHandler = GetComponent<Unity_recive_data_from_Arduino>();
    }

    public bool CheckIfConnected()
    {
        return arduinoLibraryHandler.isConnected;
    }
    public float GetSpeed()
    {
        return arduinoLibraryHandler.speed;
    }
    public float GetDirection()
    {
        return arduinoLibraryHandler.direction;
    }
    public float GetHeight()
    {
        return arduinoLibraryHandler.height;
    }
}
