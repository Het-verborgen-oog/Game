using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DolphinMovement : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectWithInputHandlers;
    //must be child of object with this script
    [SerializeField] private GameObject dolphinObject;
    private List<IInputHandler> availableInputHandlers;
    [SerializeField] public float horizontalMovementSpeed = 5;
    [SerializeField] public float verticalMovementSpeed = 5;
    [SerializeField] public float horizontalMovementLimit = 20;
    [SerializeField] public float verticalMovementLimit = 20;
    [SerializeField] public float raycastAngle = 45;
    [SerializeField] public float raycastLength = 5;
    [SerializeField] public float raycastFarLength = 6;
    [SerializeField] public float speedMultiplier = 2;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 leftVector;
    private Vector3 rightVector;
    private Vector3 upVector;
    private Vector3 downVector;

    private IInputHandler desiredInputHandler;

    private void Start()
    {
        availableInputHandlers = gameObjectWithInputHandlers.GetComponentsInChildren<IInputHandler>().ToList();
    }
    
    //Arduino is priority so you can only play with keyboard when arduino is NOT connected.
    private void CheckForAvailableInputHandler()
    {
        foreach (IInputHandler inputHandler in availableInputHandlers)
        {
            bool isConnected = inputHandler.CheckIfConnected();
            if (isConnected)
            {
                desiredInputHandler = inputHandler;
                break;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckForAvailableInputHandler();

        //gets input
        GetInput();

        //changes horizonatal input or verticalinput if the raycasts hit somethings
        LockMovement();
        AvoidCollisions();

        //moves the player acording to the inputs
        Movement();
    }

    public void GetInput()
    {
        horizontalInput = desiredInputHandler.GetXMovement();
        verticalInput = desiredInputHandler.GetYMovement();

        leftVector = Quaternion.AngleAxis(-60, transform.up) * transform.forward;
        rightVector = Quaternion.AngleAxis(60, transform.up) * transform.forward;
        upVector = Quaternion.AngleAxis(-raycastAngle, transform.right) * transform.forward;
        downVector = Quaternion.AngleAxis(raycastAngle, transform.right) * transform.forward;
    }


    private void Movement()
    {
        //Add yaw pitch and roll to the dolphin
        float yaw = Mathf.Lerp(0, 20, Mathf.Abs(horizontalInput)) * Mathf.Sign(horizontalInput);
        float pitch = Mathf.Lerp(0, 20, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        float roll = Mathf.Lerp(0, 30, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        //Move the dolphin
        dolphinObject.transform.localRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);
        transform.localPosition += new Vector3(horizontalInput * Time.deltaTime * horizontalMovementSpeed, (verticalInput * -1f) * Time.deltaTime * verticalMovementSpeed, 0);
    }

    private void AvoidCollisions()
    {
        //Vector3 offset = new(0, 10, 0);
        Ray downRay = new(transform.position, downVector);
        Ray upRay = new(transform.position, upVector);
        Ray leftRay = new(transform.position, leftVector);
        Ray rightRay = new(transform.position, rightVector);
        RaycastHit hit;

        if (Physics.Raycast(leftRay, out hit, raycastLength))
        {
            horizontalInput = 1 * Math.Min(5, 5 / hit.distance);
        }

        if (Physics.Raycast(rightRay, out hit, raycastLength))
        {
            horizontalInput = -1f * Math.Min(5, 5 / hit.distance);
        }

        if (Physics.Raycast(upRay, out hit, raycastLength))
        {
            verticalInput = 1f * Math.Min(10, 10 / hit.distance);
        }

        if (Physics.Raycast(downRay, out hit, raycastLength))
        {
            verticalInput = -1f * Math.Min(10, 10 / hit.distance);
        }
    }

    private void LockMovement()
    {
        if (Physics.Raycast(transform.position, leftVector, raycastFarLength) || transform.localPosition.x <= horizontalMovementLimit * -1f)
        {
            if (horizontalInput < 0)
            {
                horizontalInput = 0;
            }
        }

        if (Physics.Raycast(transform.position, rightVector, raycastFarLength) || transform.localPosition.x >= horizontalMovementLimit )
        {
            if (horizontalInput > 0)
            {
                horizontalInput = 0;
            }
        }

        if (Physics.Raycast(transform.position, upVector, raycastFarLength) || transform.localPosition.y >= verticalMovementLimit)
        {
            if (verticalInput < 0)
            {
                verticalInput = 0;
            }
        }

        if (Physics.Raycast(transform.position, downVector, raycastFarLength) || transform.localPosition.y <= verticalMovementLimit * -1f)
        {
            if (verticalInput > 0)
            {
                verticalInput = 0;
            }
        }
    }

    private void OnDrawGizmos()//used to see Ray in editor without update function
    {
        Debug.DrawRay(transform.position, leftVector * raycastLength, Color.red);
        Debug.DrawRay(transform.position, rightVector * raycastLength, Color.red);
        Debug.DrawRay(transform.position, upVector * raycastLength, Color.green);
        Debug.DrawRay(transform.position, downVector * raycastLength, Color.green);
    }
}
