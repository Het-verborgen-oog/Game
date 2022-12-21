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
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animationHash = Animator.StringToHash(ANIMATORBOOLEAN);
    }

    public void HidePath()
    {
        animator.SetBool(animationHash, false);
    }

    public void ShowPath()
    {
        animator.SetBool(animationHash, true);
    }
}
