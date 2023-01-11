using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundItself : MonoBehaviour, IRotatable
{
    [SerializeField] 
    private float rotationSpeed;

    // Monobehaviour Methods
    void Update()
    {
        Rotate();
    }

    // Public Methods
    public void Rotate()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }
}
