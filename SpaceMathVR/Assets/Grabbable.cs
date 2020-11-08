using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public Transform grabbableTarget;
    public Transform originalParent; // this will be set by whatever is picking it up

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
