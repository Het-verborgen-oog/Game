using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    private Animator animator;

    // Monobehaviour Methods
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Public Methods
    public void PlayAnimationTrigger(string triggerHash)
    {
        animator.SetTrigger(triggerHash);
    }
}
