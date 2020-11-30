using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainButton : MonoBehaviour
{
    public EventManager eventManager;
    private UnityAction clickListener, endListener;
    public DialReader poleDialReader;
    public Pole pole;

    void Awake()
    {
        clickListener = new UnityAction(processClick);
    }

    void Start()
    {
        MeshRenderer meshRenderer = pole.GetComponent<MeshRenderer>() as MeshRenderer;
        meshRenderer.enabled = false;
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventName.mainButtonClick, clickListener);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventName.mainButtonClick, clickListener);
    }

    void processClick()
    {
        Debug.Log("Button ProcessClick");
        if (eventManager.phase == EventManager.PhaseName.intro)
        {
            Debug.Log("Button ProcessClick Intro");
            EventManager.TriggerEvent(EventManager.EventName.challenge);
        } else if (eventManager.phase == EventManager.PhaseName.challenge) {
            Debug.Log("Button ProcessClick challenge");
            // move and resize the pole
            float poleLength = poleDialReader.value * 0.1f - 0.05f;
            pole.userLength = (int)poleDialReader.value;

            pole.transform.SetPositionAndRotation(pole.spawnLocation.position, pole.spawnLocation.rotation);
            pole.transform.localScale = new Vector3(0.05f, poleLength, 0.05f);
            pole.GetComponent<MeshRenderer>().enabled = true;
            Debug.Log("POLE CREATED!");
        } else if (eventManager.phase == EventManager.PhaseName.victory)
        {
            Debug.Log("Button ProcessClick victory");
            EventManager.TriggerEvent(EventManager.EventName.end);
        }
    }
}
