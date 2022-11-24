using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundItself : MonoBehaviour, IRotatable
{
    [SerializeField] private float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }
}
