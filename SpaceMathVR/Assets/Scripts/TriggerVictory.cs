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
    private bool victory = false;

    void Awake()
    {
        unityAction = new UnityAction(hidePole);
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
        if (!victory && Vector3.Distance(transform.position, pole.transform.position) < distanceThreshold && pole.userLength == goalLength)
        {
            EventManager.TriggerEvent(EventManager.EventName.victory);
            victory = true;
            gameObject.GetComponent<MeshRenderer>().material = green;
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
