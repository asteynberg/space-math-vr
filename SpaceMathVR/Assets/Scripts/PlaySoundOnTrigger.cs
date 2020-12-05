using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    private UnityAction playAction;
    public EventManager.EventName eventName;

    void Awake()
    {
        playAction = new UnityAction(playSound);
    }

    // make sure you start listening on enable, and stop listening on disable
    // this should take the form of EventManager.<Start or Stop>Listening(<event name>, <action>)
    void OnEnable()
    {
        EventManager.StartListening(eventName, playAction);
    }

    void OnDisable()
    {
        EventManager.StopListening(eventName, playAction);
    }

    void playSound()
    {
        // if it's either not playing or what it is playing is not our clip, play our clip
        if (!audioSource.isPlaying || audioSource.clip != audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

}
