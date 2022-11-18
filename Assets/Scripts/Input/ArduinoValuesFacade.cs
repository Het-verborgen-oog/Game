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
    public int GetSpeed()
    {
        return arduinoLibraryHandler.speed;
    }
    public int GetDirection()
    {
        return arduinoLibraryHandler.direction;
    }
    public int GetHeight()
    {
        return arduinoLibraryHandler.height;
    }
}
