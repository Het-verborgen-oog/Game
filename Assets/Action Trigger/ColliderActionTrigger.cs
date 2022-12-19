using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ColliderActionTrigger : MonoBehaviour
{
    [SerializeField]
    private List<MonoBehaviour> actions;

    private List<ITrigger> triggers;

    private void Start() {
        triggers = new List<ITrigger>();
        foreach(MonoBehaviour script in actions)
        {
            if (script is ITrigger)
            {
                triggers.Add(script as ITrigger);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        foreach(ITrigger trigger in triggers)
        {
            trigger.TriggerEvent(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        foreach(ITrigger trigger in triggers)
        {
            trigger.TriggerEvent(false);
        }
    }
}
