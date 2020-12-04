using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// make sure you import UnityEngine.Events
using UnityEngine.Events;

public class SimpleSound : MonoBehaviour
{
    public AudioSource audioSource;
    public EventManager.EventName startEvent;
    public EventManager.EventName endEvent;

    // define actions here
    public UnityAction playAction, stopAction;

    // first, define what you'd like to do when different events trigger
    void playSound()
    {
        audioSource.Play();
    }

    void stopSound()
    {
        audioSource.Stop();
    }

    // during Awake, set your UnityActions to be unityActionName = UnityAction(<function to run when event triggers>)
    void Awake()
    {
        playAction = new UnityAction(playSound);
        stopAction = new UnityAction(stopSound);
    }

    // make sure you start listening on enable, and stop listening on disable
    // this should take the form of EventManager.<Start or Stop>Listening(<event name>, <action>)
    void OnEnable()
    {
        // when the challenge begins, start playing
        EventManager.StartListening(EventManager.EventName.challenge, playAction);
        // when the player is victorious, stop playing
        EventManager.StartListening(EventManager.EventName.victory, stopAction);
    }

    void OnDisable()
    {
        // duplicate from OnEnable and change them to "StopListening"
        EventManager.StopListening(EventManager.EventName.challenge, playAction);
        EventManager.StopListening(EventManager.EventName.victory, stopAction);
    }

    void Start()
    {
        // if your start event is the "none" event, 
        if (startEvent == EventManager.EventName.none)
        {
            playSound();
        }
    }
}
