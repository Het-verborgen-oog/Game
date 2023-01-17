using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlpool : MonoBehaviour
{
    
    [SerializeField]
    private float spinOutTime = 1f;

    private const string spinningTrigger = "IsSpinning";
    const string PLAYERTAG = "Player";

    // Monobehaviour Methods        
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYERTAG))
        {
            //AnimatorController animatorController = other.GetComponentInChildren<AnimatorController>();
            DolphinMovement dolf = other.GetComponentInChildren<DolphinMovement>();
            StartCoroutine(Spinout(dolf));
        }
    }

    // Private Methods
    private IEnumerator Spinout(DolphinMovement player)
    {
        player.ToggleMovement(false);
        //animatorController.PlayAnimationTrigger(spinningTrigger);
        yield return new WaitForSeconds(spinOutTime);
        player.ToggleMovement(true);
    }
}
