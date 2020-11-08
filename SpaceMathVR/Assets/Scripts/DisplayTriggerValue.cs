using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTriggerValue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float leftHandTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        gameObject.GetComponent<TextMesh>().text = leftHandTrigger.ToString();
    }
}
