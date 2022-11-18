using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputHandler : MonoBehaviour, IInputHandler
{
    private bool isConnected = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckIfConnected()
    {
        return isConnected;
    }

    public int GetSpeed()
    {
        return 0;
    }

    public int GetXMovement()
    {
        return 0;
    }

    public int GetYMovement()
    {
        return 0;
    }
}
