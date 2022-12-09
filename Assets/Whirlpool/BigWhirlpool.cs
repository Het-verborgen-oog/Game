using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWhirlpool : MonoBehaviour
{
    [SerializeField]
    private SideTrack trackInsideWhirlPool;
    private void OnTriggerEnter(Collider other)
    {
        Offset offset = other.GetComponentInChildren<Offset>();
        if (offset == null) return;

        CartController playerDollyCartController = offset.GetComponentInParent<CartController>();
        playerDollyCartController.SetAltTrackAsMainTrack(trackInsideWhirlPool);
    }
}
