using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlpool : MonoBehaviour
{
    private const string spinningTrigger = "IsSpinning";
    private void OnTriggerEnter(Collider other)
    {
        Offset offset = other.GetComponentInChildren<Offset>();

        AnimatorController animatorController = offset.GetComponentInChildren<AnimatorController>();
        Debug.Log($"{offset}, {other}, {animatorController}");
        StartCoroutine(Spinout(offset, animatorController));
    }

    IEnumerator Spinout(Offset player, AnimatorController animatorController)
    {
        player.ChangeInputAccessibility(false);
        animatorController.PlayAnimationTrigger(spinningTrigger);
        yield return new WaitForSeconds(1f);
        player.ChangeInputAccessibility(true);
    }
}
