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
    // Start is called before the first frame update
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

    public void Trigger()
    {
        PlayerEnteredSplitPath.Invoke(parentSplitPath);
    }
}
