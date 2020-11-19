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

    private List<GameObject> interactable = new List<GameObject>();
    private List<Transform> turnable = new List<Transform>();
    // List of if the controller is at this Y value, then
    private List<float> turnableZeros = new List<float>();

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
        if (interactable.Count > 0)
        {
            float trigger = OVRInput.Get(this.button);
            if (grabbing)
            {
                if (trigger < triggerThreshold)
                {
                    // drop the items
                    foreach (GameObject thing in interactable)
                    {
                        Debug.Log("About to try to drop something");
                        thing.transform.SetParent(thing.GetComponent<Interactable>().originalParent);
                        Debug.Log("Dropped an object!");
                    }
                    // remove all the fixedPosition objects from the list
                    turnable = new List<Transform>();
                    turnableZeros = new List<float>();
                    grabbing = false;
                    Debug.Log("Dropped everything!");
                } else
                {
                    // we're still holding on to the objects
                    for (int i = 0; i < turnableZeros.Count; i++)
                    {
                        // do all of the turning
                        Vector3 rotation = turnable[i].transform.eulerAngles;
                        //Debug.Log("Rotation:");
                        //Debug.Log(rotation);
                        float targetY = gameObject.transform.eulerAngles.y - turnableZeros[i];
                        //Debug.Log("Controller y:");
                        //Debug.Log(gameObject.transform.rotation.y);
                        //Debug.Log("The zero:");
                        //Debug.Log(turnableZeros[i]);
                        //Debug.Log("The result:");
                        //Debug.Log(targetY);
                        turnable[i].transform.eulerAngles = new Vector3(rotation.x, targetY, rotation.z);
                    }
                }
            }
            else
            {
                if (trigger >= triggerThreshold)
                {
                    // pick up the objects
                    foreach (GameObject thing in interactable)
                    {
                        Interactable interactableComponent = thing.GetComponent<Interactable>();
                        
                        Debug.Log("Picked up an object!");
                        if (interactableComponent.interactionType == Interactable.InteractionType.turn)
                        {
                            Debug.Log("Recognized that the object was turnable.");
                            turnable.Add(interactableComponent.interactionTarget);
                            turnableZeros.Add(gameObject.transform.eulerAngles.y - interactableComponent.interactionTarget.transform.eulerAngles.y);
                        }
                        else if (interactableComponent.interactionType == Interactable.InteractionType.grab)
                        {
                            Debug.Log("Recognized that the object was grabbable");
                            interactableComponent.interactionTarget.transform.SetParent(gameObject.transform);
                        }
                        else
                        {
                            Debug.Log("Decided that the object was clickable");
                            Debug.Log("Triggered the clickable object");
                            EventManager.TriggerEvent(interactableComponent.triggerEventName);
                        }
                    }
                    grabbing = true;
                    Debug.Log("Picked everything up!");
                }
            }
        }


        if (interactable.Count == 0) {
            // there's nothing to pick up
            gameObject.GetComponent<MeshRenderer>().material = blue;
        } else if (grabbing)
        {
            // you're currently holding something
            gameObject.GetComponent<MeshRenderer>().material = yellow;
        } else
        {
            // there's stuff to pick up or click
            gameObject.GetComponent<MeshRenderer>().material = green;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered enter!");
        Interactable interactableComponent = other.gameObject.GetComponent<Interactable>();
        if (interactableComponent != null)
        {
            interactable.Add(other.gameObject);
            interactableComponent.originalParent = other.gameObject.transform.parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Triggered exit!");
        // will just return false if it's not in there
        interactable.Remove(other.gameObject);
    }
}
