using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputHandler : MonoBehaviour, IInputHandler
{
    private bool isConnected = true;

    public bool CheckIfConnected()
    {
        return isConnected;
    }

    public float GetSpeed()
    {
        return 2;
    }

    public float GetXMovement()
    {
        float xMovement = Input.GetAxis("Horizontal");
        return xMovement;
    }

    public float GetYMovement()
    {
        float yMovement = Input.GetAxis("Vertical");
        return yMovement;
    }
}
