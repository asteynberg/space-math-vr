using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public enum Hand {Left, Right};
    public Hand hand;
    public Material green;
    public Material blue;
    public Material yellow;
    private OVRInput.Axis1D button;
    private bool grabbing;
    private float triggerThreshold = 0.2f;

    private List<GameObject> grabbable = new List<GameObject>();

    private void Awake()
    {
        if (hand == Hand.Left)
        {
            this.button = OVRInput.Axis1D.PrimaryIndexTrigger;
        }
        else
        {
            this.button = OVRInput.Axis1D.SecondaryIndexTrigger;
        }
        grabbing = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = blue;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbable.Count > 0)
        {
            float trigger = OVRInput.Get(this.button);
            if (grabbing)
            {
                
                if (trigger < triggerThreshold)
                {
                    // drop the items
                    foreach (GameObject thing in grabbable)
                    {
                        Debug.Log("About to try to drop something");
                        thing.transform.SetParent(thing.GetComponent<Grabbable>().originalParent);
                        Debug.Log("Dropped an object!");
                    }
                    grabbing = false;
                    Debug.Log("Dropped everything!");
                }
            }
            else
            {
                if (trigger >= triggerThreshold)
                {
                    foreach (GameObject thing in grabbable)
                    {
                        thing.GetComponent<Grabbable>().grabbableTarget.transform.SetParent(gameObject.transform);
                        Debug.Log("Picked up an object!");
                    }
                    grabbing = true;
                    Debug.Log("Picked everything up!");
                }
            }
        }


        if (grabbable.Count == 0) {
            // there's nothing to pick up
            gameObject.GetComponent<MeshRenderer>().material = blue;
        } else if (grabbing)
        {
            // you're currently holding something
            gameObject.GetComponent<MeshRenderer>().material = yellow;
        } else
        {
            // there's stuff to pick up
            gameObject.GetComponent<MeshRenderer>().material = green;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered enter!");
        Grabbable grabbableComponent = other.gameObject.GetComponent<Grabbable>();
        if (grabbableComponent != null)
        {
            grabbable.Add(other.gameObject);
            grabbableComponent.originalParent = other.gameObject.transform.parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Triggered exit!");
        // will just return false if it's not in there
        grabbable.Remove(other.gameObject);
    }
}
