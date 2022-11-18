using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    bool CheckIfConnected();
    int GetSpeed();
    int GetXMovement();
    int GetYMovement();
}
