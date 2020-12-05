using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerVictory : MonoBehaviour
{
    private UnityAction unityAction;
    public EventManager eventManager;
    public GrabManager grabManager;
    public Pole pole;
    public float distanceThreshold = 0.4f;
    public int goalLength = 13;
    public Material green;
    public Material red;
    public Material transparentBlue;
    private enum Status { nothing, failure, victory };
    private Status status = Status.nothing;

    void Awake()
    {
        unityAction = new UnityAction(hidePole);
        gameObject.GetComponent<MeshRenderer>().material = transparentBlue;
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventName.victory, unityAction);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventName.victory, unityAction);
    }


    // Update is called once per frame
    void Update()
    {
        if (status == Status.nothing && Vector3.Distance(transform.position, pole.transform.position) < distanceThreshold)
        {
            if (pole.userLength == goalLength)
            {
                // if the user uses the right size pole
                status = Status.victory;
                gameObject.GetComponent<MeshRenderer>().material = green;
                EventManager.TriggerEvent(EventManager.EventName.victory);
            } else
            {
                // if the user uses the pole of the wrong size
                status = Status.failure;
                gameObject.GetComponent<MeshRenderer>().material = red;
                EventManager.TriggerEvent(EventManager.EventName.failure);
            }
        } else if (status == Status.failure && Vector3.Distance(transform.position, pole.transform.position) >= distanceThreshold) {
            // if the user pulls the wrong pole back
            status = Status.nothing;
            gameObject.GetComponent<MeshRenderer>().material = transparentBlue;
            
        }
    }

    void hidePole()
    {
        Debug.Log("Disabling Pole");
        grabManager.disableInteraction(pole.GetComponent<Interactable>());
        Destroy(pole.gameObject);
        Debug.Log("Pole Destroyed");
    }
}
