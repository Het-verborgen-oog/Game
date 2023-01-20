using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float DestroyAfterSeconds;

    // Monobehaviour Methods
    void Start()
    {
        StartCoroutine(BeginCountdown());
    }

    // Private Methods
    IEnumerator BeginCountdown()
    {
        yield return new WaitForSeconds(DestroyAfterSeconds);
        Destroy(gameObject);
    }
}
