using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationGuidance : MonoBehaviour, IVisualGuidance
{
    [SerializeField]
    private Animator animator;
    private const string ANIMATORBOOLEAN = "onDifferentPath";
    private int animationHash;
    
    // Monobehaviour Methods
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animationHash = Animator.StringToHash(ANIMATORBOOLEAN);
    }

    // Public Methods
    public void HidePath()
    {
        animator.SetBool(animationHash, false);
    }

    public void PausePath()
    {
        return;
    }

    public void ShowPath()
    {
        animator.SetBool(animationHash, true);
    }
}
