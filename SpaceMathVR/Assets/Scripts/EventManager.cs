using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    private UnityAction challengeAction, victoryAction, endAction;

    public enum PhaseName {intro, challenge, victory, end};

    public PhaseName phase = PhaseName.intro;

    public enum EventName {none, mainButtonClick, dialClick, challenge, victory, end};

    private Dictionary<EventName, UnityEvent> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<EventName, UnityEvent>();
        }
    }

    void Awake()
    {
        challengeAction = new UnityAction(challengePhaseChanger);
        victoryAction = new UnityAction(victoryPhaseChanger);
        endAction = new UnityAction(endPhaseChanger);
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventName.challenge, challengeAction);
        EventManager.StartListening(EventManager.EventName.victory, victoryAction);
        EventManager.StartListening(EventManager.EventName.end, endAction);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventName.challenge, challengeAction);
        EventManager.StopListening(EventManager.EventName.victory, victoryAction);
        EventManager.StopListening(EventManager.EventName.end, endAction);
    }

    private void challengePhaseChanger()
    {
        Debug.Log("Challenge phase triggered");
        phase = PhaseName.challenge;
    }
    private void victoryPhaseChanger()
    {
        Debug.Log("Victory phase triggered");
        phase = PhaseName.victory;
    }

    private void endPhaseChanger()
    {
        Debug.Log("End phase triggered");
        phase = PhaseName.end;
    }

    public static void StartListening(EventName eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(EventName eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(EventName eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}