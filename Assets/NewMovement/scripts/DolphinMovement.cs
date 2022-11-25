using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DolphinMovement : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectWithInputHandlers;
    private List<IInputHandler> availableInputHandlers;
    [SerializeField] public float horizontalMovmentSpeed = 5;
    [SerializeField] public float verticalMovmentSpeed = 5;
    [SerializeField] public float raycastAngle = 45;
    [SerializeField] public float raycastLength = 5;
    [SerializeField] public float raycastFarLength = 6;
    [SerializeField] public float speedMultiplier = 2;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 leftRay;
    private Vector3 rightRay;
    private Vector3 upRay;
    private Vector3 downRay;

    private IInputHandler desiredInputHandler;

    private void Start()
    {
        availableInputHandlers = gameObjectWithInputHandlers.GetComponentsInChildren<IInputHandler>().ToList();
        leftRay = Quaternion.AngleAxis(-raycastAngle, transform.up) * transform.forward;
        rightRay = Quaternion.AngleAxis(raycastAngle, transform.up) * transform.forward;
        upRay = Quaternion.AngleAxis(-raycastAngle, transform.right) * transform.forward;
        downRay = Quaternion.AngleAxis(raycastAngle, transform.right) * transform.forward;
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
        AvoidColisions();
        //moves the player acording to the inputs
        Movement();
    }

    public void GetInput()
    {
        //TODO check if ardunio is online
        if (false)
        {
            horizontalInput = desiredInputHandler.GetXMovement();
            verticalInput = desiredInputHandler.GetYMovement();
        }
        else
        {

            //up = -1 and down = 1
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }
    }


    private void Movement()
    {
        //Add yaw pitch and roll to the dolphin
        float yaw = Mathf.Lerp(0, 20, Mathf.Abs(horizontalInput)) * Mathf.Sign(horizontalInput);
        float pitch = Mathf.Lerp(0, 20, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        float roll = Mathf.Lerp(0, 30, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);
        transform.localRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);

        //Move the dolphin
        //transform.position += transform.up * verticalInput * Time.deltaTime;
        //transform.position += transform.right * horizontalInput * Time.deltaTime;
        transform.position += new Vector3(horizontalInput * Time.deltaTime * horizontalMovmentSpeed, (verticalInput * -1f) * Time.deltaTime * verticalMovmentSpeed, 0);
    }

    private void AvoidColisions()
    {
        if (Physics.Raycast(transform.position, leftRay, raycastLength))
        {
            Debug.Log("left Ray");
            horizontalInput = 1 * speedMultiplier;
        }

        if (Physics.Raycast(transform.position, rightRay, raycastLength))
        {
            Debug.Log("right Ray");
            horizontalInput = -1f * speedMultiplier;
        }

        if (Physics.Raycast(transform.position, upRay, raycastLength))
        {
            Debug.Log("up Ray");
            verticalInput = 1f * speedMultiplier;
        }

        if (Physics.Raycast(transform.position, downRay, raycastLength))
        {
            Debug.Log("down Ray");
            verticalInput = -1f * speedMultiplier;
        }
    }

    private void LockMovement()
    {
        if (Physics.Raycast(transform.position, leftRay, raycastFarLength))
        {
            if (horizontalInput < 0)
            {
                horizontalInput = 0;
            }
        }

        if (Physics.Raycast(transform.position, rightRay, raycastFarLength))
        {
            if (horizontalInput > 0)
            {
                horizontalInput = 0;
            }
        }

        if (Physics.Raycast(transform.position, upRay, raycastFarLength))
        {
            if (verticalInput < 0)
            {
                verticalInput = 0;
            }
        }

        if (Physics.Raycast(transform.position, downRay, raycastFarLength))
        {
            if (verticalInput > 0)
            {
                verticalInput = 0;
            }
        }
    }

    private void OnDrawGizmos()//used to see Ray in editor without update function
    {
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.red);
        Debug.DrawRay(transform.position, leftRay * raycastLength, Color.red);
        Debug.DrawRay(transform.position, rightRay * raycastLength, Color.red);
        Debug.DrawRay(transform.position, upRay * raycastLength, Color.green);
        Debug.DrawRay(transform.position, downRay * raycastLength, Color.green);
    }
}
