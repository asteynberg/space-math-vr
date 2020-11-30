using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterOnChallenge : MonoBehaviour
{
    public UnityAction unityAction;

    void Awake()
    {
        unityAction = new UnityAction(show);
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventName.challenge, unityAction);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventName.challenge, unityAction);
    }

    void show()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
