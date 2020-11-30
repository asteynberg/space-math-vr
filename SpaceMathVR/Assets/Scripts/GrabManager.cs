using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabManager : MonoBehaviour
{
    public class TurnInfo
    {
        public Interactable interactable;
        public float turnZero;
    }

    public class GrabInfo
    {
        public List<Interactable> hovering = new List<Interactable>();
        public List<Interactable> grabbing = new List<Interactable>();
        public List<Interactable> turning = new List<Interactable>();
        public List<float> turningZeros = new List<float>();
    }

    public List<Grabber> grabbers = new List<Grabber>();
    public List<GrabInfo> data = new List<GrabInfo>();
    public Dictionary<Interactable, int> controllers = new Dictionary<Interactable, int>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GrabManager start running...");
        Debug.Log("Expecting " + grabbers.Count);
        // initialize all of the grabbers with a unique number
        // UPGRADE ideally there would be a registration process where a grabber would contact the manager to recieve a unique ID
        // ...but this does the trick for the time being.
        for (int i = 0; i < grabbers.Count; i++) {
            Debug.Log(i);
            Grabber grabber = grabbers[i];
            grabber.grabberNumber = i;
            grabber.grabManager = gameObject.GetComponent<GrabManager>() as GrabManager;
            data.Add(new GrabInfo());
            Debug.Log("Added grabber " + i);
        }
    }

    void Update()
    {
        for (int grabberNumber = 0; grabberNumber < data.Count; grabberNumber++)
        {
            GrabInfo grabInfo = data[grabberNumber];
            Grabber grabber = grabbers[grabberNumber];
            for (int objectIndex = 0; objectIndex < grabInfo.turning.Count; objectIndex++)
            {
                Interactable interactable = grabInfo.turning[objectIndex];
                float turningZero = grabInfo.turningZeros[objectIndex];
                Transform interactionTarget = interactable.interactionTarget;
                Vector3 rotation = interactionTarget.eulerAngles;
                float targetY = grabber.transform.eulerAngles.y - turningZero;
                interactionTarget.eulerAngles = new Vector3(rotation.x, targetY, rotation.z);
            }
        }
        // do all of the turning
        //Vector3 rotation = turnable[i].transform.eulerAngles;
        //Debug.Log("Rotation:");
        //Debug.Log(rotation);
        //float targetY = gameObject.transform.eulerAngles.y - turnableZeros[i];
        //Debug.Log("Controller y:");
        //Debug.Log(gameObject.transform.rotation.y);
        //Debug.Log("The zero:");
        //Debug.Log(turnableZeros[i]);
        //Debug.Log("The result:");
        //Debug.Log(targetY);
        //turnable[i].transform.eulerAngles = new Vector3(rotation.x, targetY, rotation.z);
    }

    // removes control from the grabber (does not put object into hovering)
    public void removeControl(int grabberNumber, Interactable interactable)
    {
        GrabInfo grabInfo = data[grabberNumber];

        int grabbingIndex = grabInfo.grabbing.IndexOf(interactable);
        if (grabbingIndex != -1)
        {
            interactable.transform.SetParent(interactable.originalParent);
            grabInfo.grabbing.Remove(interactable);
        }
        
        int turningIndex = grabInfo.turning.IndexOf(interactable);
        if (turningIndex != -1)
        {
            grabInfo.turning.RemoveAt(turningIndex);
            grabInfo.turningZeros.RemoveAt(turningIndex);
        }
        controllers.Remove(interactable);
    }

    public void processTriggerEnter(int grabberNumber, Interactable grabbed)
    {
        data[grabberNumber].hovering.Add(grabbed);
    }

    public void processTriggerExit(int grabberNumber, Interactable grabbed)
    {
        data[grabberNumber].hovering.Remove(grabbed);
    }

    public void processGrabAction(int grabberNumber)
    {
        GrabInfo grabInfo = data[grabberNumber];
        foreach (Interactable thing in grabInfo.hovering)
        {
            if (controllers.ContainsKey(thing))
            {
                // another controller is in control - remove that control
                int prevGrabberNumber = controllers[thing];
                removeControl(prevGrabberNumber, thing);
                Debug.Log("Removed control from another grabber");
            }

            
            Debug.Log("Grabbed an object!");
            if (thing.interactionType == Interactable.InteractionType.trigger)
            {
                Debug.Log("Triggered the clickable object");
                EventManager.TriggerEvent(thing.triggerEventName);
            } else
            {
                // only control it if it's not a trigger
                controllers[thing] = grabberNumber;

                if (thing.interactionType == Interactable.InteractionType.turn)
                {
                    Debug.Log("Recognized that the object was turnable.");
                    grabInfo.turning.Add(thing);
                    grabInfo.turningZeros.Add(grabbers[grabberNumber].transform.eulerAngles.y - thing.interactionTarget.transform.eulerAngles.y);
                }
                else
                {
                    // (thing.interactionType == Interactable.InteractionType.grab)
                    Debug.Log("Recognized that the object was grabbable (pick-uppable)");
                    grabInfo.grabbing.Add(thing);
                    thing.interactionTarget.transform.SetParent(grabbers[grabberNumber].transform);
                }
            }
        }
    }
    
    public void processReleaseAction(int grabberNumber)
    {
        Debug.Log("Starting to process release action");
        GrabInfo grabInfo = data[grabberNumber];
        while (controlCount(grabberNumber) > 0)
        {
            if (grabInfo.grabbing.Count > 0)
            {
                removeControl(grabberNumber, grabInfo.grabbing[0]);
            }

            if (grabInfo.turning.Count > 0)
            {
                removeControl(grabberNumber, grabInfo.turning[0]);
            }
        }
        Debug.Log("Processed release action!");
    }

    public int controlCount(int grabberNumber)
    {
        GrabInfo grabInfo = data[grabberNumber];
        return grabInfo.grabbing.Count + grabInfo.turning.Count;
    }

    public int hoverCount(int grabberNumber)
    {
        GrabInfo grabInfo = data[grabberNumber];
        return grabInfo.hovering.Count;
    }
}
