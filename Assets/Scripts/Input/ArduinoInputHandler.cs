using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoInputHandler : MonoBehaviour, IInputHandler
{
    [SerializeField]
    private ArduinoValuesFacade facade;

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
        return facade.CheckIfConnected();
    }

    public int GetSpeed()
    {
        return facade.GetSpeed();
    }

    public int GetXMovement()
    {
        return facade.GetDirection();
    }

    public int GetYMovement()
    {
        return facade.GetHeight();
    }

}
