using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolRotatingObject : MonoBehaviour, IRotatable
{
    [Header("Rotation Speed")]
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
    [SerializeField] private float zSpeed;
    [SerializeField] private float speedMultiplier;

    public void Rotate()
    {
        transform.Rotate(new Vector3(xSpeed, ySpeed, zSpeed)* speedMultiplier * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }


}

