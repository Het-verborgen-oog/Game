using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SplitPathTrigger : MonoBehaviour,IInteractable
{
    [SerializeField]
    private SplitPath parentSplitPath;

    public delegate void OnPlayerEnterSplitPath(SplitPath splitPath);
    public static OnPlayerEnterSplitPath PlayerEnteredSplitPath;
    
    // Monobehaviour Methods
    void Start()
    {
        parentSplitPath = GetComponentInParent<SplitPath>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Trigger();
        }
    }

    // Public Methods
    public void Trigger()
    {
        PlayerEnteredSplitPath.Invoke(parentSplitPath);
    }
}
