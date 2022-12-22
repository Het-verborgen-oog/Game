using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlpool : MonoBehaviour
{
    private const string spinningTrigger = "IsSpinning";
    const string PLAYERTAG = "Player";
    [SerializeField]
    private float spinOutTime = 1f;

    //Replace the Offset with the current way to control the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYERTAG))
        {
            AnimatorController animatorController = other.GetComponentInChildren<AnimatorController>();
            DolphinMovement dolf = other.GetComponentInChildren<DolphinMovement>();
            StartCoroutine(Spinout(dolf, animatorController));
        }
    }

    IEnumerator Spinout(DolphinMovement player, AnimatorController animatorController)
    {
        player.ToggleMovement(false);
        animatorController.PlayAnimationTrigger(spinningTrigger);
        yield return new WaitForSeconds(spinOutTime);
        player.ToggleMovement(true);
    }
}
