using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrieveArduinoValues : MonoBehaviour
{
    
    private Unity_recive_data_from_Arduino inputArduinoHandler;

    private void Start()
    {
        inputArduinoHandler = GetComponent<Unity_recive_data_from_Arduino>();
    }

    public bool CheckIfConnected()
    {
        return inputArduinoHandler.isConnected;
    }
    public int GetSpeed()
    {
        return inputArduinoHandler.speed;
    }
    public int GetDirection()
    {
        return inputArduinoHandler.direction;
    }
    public int GetHeight()
    {
        return inputArduinoHandler.height;
    }
}
