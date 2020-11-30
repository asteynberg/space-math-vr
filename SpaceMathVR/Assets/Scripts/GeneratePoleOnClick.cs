using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneratePoleOnClick : MonoBehaviour
{
    private UnityAction clickListener;
    public DialReader poleDialReader;
    public Pole pole;

    void Awake()
    {
        clickListener = new UnityAction(createPole);
    }

    void Start()
    {
        MeshRenderer meshRenderer = pole.GetComponent<MeshRenderer>() as MeshRenderer;
        meshRenderer.enabled = false;
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
        // move and resize the pole
        float poleLength = poleDialReader.value * 0.1f - 0.05f;
        pole.userLength = (int) poleDialReader.value;
        
        pole.transform.SetPositionAndRotation(pole.spawnLocation.position, pole.spawnLocation.rotation);
        pole.transform.localScale = new Vector3(0.05f, poleLength, 0.05f);
        pole.GetComponent<MeshRenderer>().enabled = true;
        Debug.Log("POLE CREATED!");
    }
}
