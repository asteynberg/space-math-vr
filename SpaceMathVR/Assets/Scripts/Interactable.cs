using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform interactionTarget;
    [HideInInspector]
    public Transform originalParent; // this will be set by whatever is picking it up
    // if true, then the object won't move position - it will just rotate along its y axis
    public enum InteractionType {grab, turn, trigger};
    // this is used for dials
    public InteractionType interactionType;
    // if it is a trigger, what event should trigger when it's clicked?
    public EventManager.EventName triggerEventName;

    // Start is called before the first frame update
    void Start()
    {
        if (interactionTarget == null)
        {
            interactionTarget = gameObject.transform;
        }
    }
}
