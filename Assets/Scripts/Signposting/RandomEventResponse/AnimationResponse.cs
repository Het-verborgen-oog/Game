using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationResponse : MonoBehaviour, IResponse
{
    private Animator animator;
    private string animationName = "isActivated";
    private int animationHashId;
    
    // Monobehaviour Methods
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animationHashId = Animator.StringToHash(animationName);
    }

    // Public Methods
    public void Activate()
    {
        animator.SetTrigger(animationHashId);
    }

    public void ResetResponse()
    {
        animator.Play("Idle");
    }
}
