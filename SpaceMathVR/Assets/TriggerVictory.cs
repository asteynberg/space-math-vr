using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVictory : MonoBehaviour
{
    public EventManager eventManager;
    public Pole pole;
    public float distanceThreshold = 0.2f;
    public int goalLength = 13;
    public Material green;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, pole.transform.position) < distanceThreshold && pole.userLength == goalLength)
        {
            EventManager.TriggerEvent(EventManager.EventName.victory);
            gameObject.GetComponent<MeshRenderer>().material = green;
        }
    }
}
