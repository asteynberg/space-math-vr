using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public Transform grabbableTarget;
    [HideInInspector]
    public Transform originalParent; // this will be set by whatever is picking it up
    // if true, then the object won't move position - it will just rotate along its y axis
    // this is used for dials
    public bool turnable;

    // Start is called before the first frame update
    void Start()
    {
        if (grabbableTarget == null)
        {
            grabbableTarget = gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
