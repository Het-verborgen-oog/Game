using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    bool CheckIfConnected();
    float GetSpeed();
    float GetXMovement();
    float GetYMovement();
}
