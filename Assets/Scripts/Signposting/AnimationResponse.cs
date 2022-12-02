using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationResponse : MonoBehaviour, IResponse
{
    private Animator animator;
    [SerializeField]
    private string animationName;
    private int animationHashId;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animationHashId = Animator.StringToHash(animationName);
    }

    public void Activate()
    {
        animator.SetTrigger(animationHashId);
    }

    public void Reset()
    {
        
    }
}
