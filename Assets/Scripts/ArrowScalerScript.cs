using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScalerScript : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve heightGraph;

    [SerializeField]
    private Transform playerPos;

    // Monobihaviour Methods
    void Update()
    {
        CalculatePlayerDistance();
    }

    // Private Methods
    private void CalculatePlayerDistance()
    {
        float dist = Vector3.Distance(transform.position, playerPos.position);

        float size = heightGraph.Evaluate(dist * 0.01f);
        transform.localScale = new Vector3(size * 50, size * 50, size * 50);
    }
}