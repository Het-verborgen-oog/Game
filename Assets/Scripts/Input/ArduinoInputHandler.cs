using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoInputHandler : MonoBehaviour, IInputHandler
{
    [SerializeField]
    private ArduinoValuesFacade facade;

    // Public Methods
    public bool CheckIfConnected()
    {
        return facade.CheckIfConnected();
    }

    public float GetSpeed()
    {
        return facade.GetSpeed();
    }

    public float GetXMovement()
    {
        return facade.GetDirection();
    }

    public float GetYMovement()
    {
        return facade.GetHeight();
    }

}
