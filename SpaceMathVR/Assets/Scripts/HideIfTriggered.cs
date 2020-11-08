using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideIfTriggered : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Getting trigger and grip axes
        float leftHandTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float rightHandGrip = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
        Debug.Log("leftHandTrigger: " + leftHandTrigger);
        Debug.Log("rightHandGrip: " + leftHandTrigger);
        gameObject.SetActive((leftHandTrigger > 0));
    }
}
