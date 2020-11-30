using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public enum Hand { Left, Right };
    public Hand hand;
    public Material green;
    public Material blue;
    public Material yellow;
    private OVRInput.Axis1D button;
    private bool grabbing;
    private float triggerThreshold = 0.2f;

    // these will be set by the grab manager
    [HideInInspector]
    public int grabberNumber;
    [HideInInspector]
    public GrabManager grabManager;

    private void Awake()
    {
        if (hand == Hand.Left)
        {
            button = OVRInput.Axis1D.PrimaryIndexTrigger;
        }
        else
        {
            button = OVRInput.Axis1D.SecondaryIndexTrigger;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = blue;
        grabbing = checkTrigger();
    }

    void Update()
    {
        if (!grabManager) return;
        // update the color and update any trigger events

        bool triggered = checkTrigger();
        if (!grabbing && triggered)
        {
            grabbing = true;
            grabManager.processGrabAction(grabberNumber);
        }
        else if (grabbing && !triggered)
        {
            grabbing = false;
            grabManager.processReleaseAction(grabberNumber);
        }

        if (grabManager.controlCount(grabberNumber) > 0)
        {
            gameObject.GetComponent<MeshRenderer>().material = green;
        }
        else if (grabManager.hoverCount(grabberNumber) > 0)
        {
            gameObject.GetComponent<MeshRenderer>().material = yellow;
        } else
        {
            gameObject.GetComponent<MeshRenderer>().material = blue;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Debug #1");
        Interactable interactable = other.gameObject.GetComponent<Interactable>();
        Debug.Log("Debug #2");
        if (interactable)
        {
            Debug.Log("Debug #3");
            grabManager.processTriggerEnter(grabberNumber, interactable);
            Debug.Log("Triggered enter!");
        }
        Debug.Log("Debug #4");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Debug #5");
        Interactable interactable = other.gameObject.GetComponent<Interactable>();
        Debug.Log("Debug #6");
        if (interactable)
        {
            Debug.Log("Debug #7");
            grabManager.processTriggerExit(grabberNumber, interactable);
            Debug.Log("Triggered exit!");
        }
        Debug.Log("Debug #8");
    }

    private bool checkTrigger()
    {
        return OVRInput.Get(button) >= triggerThreshold;
    }
}
