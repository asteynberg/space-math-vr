using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneratePoleOnClick : MonoBehaviour
{
    public const string makePoleButtonClickEventName = "pole_button_click";
    private UnityAction clickListener;

    void Awake()
    {
        clickListener = new UnityAction(createPole);
    }
    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventName.makePoleButtonClicked, clickListener);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventName.makePoleButtonClicked, clickListener);
    }

    void createPole()
    {
        Debug.Log("POLE CREATED!");
    }
}
