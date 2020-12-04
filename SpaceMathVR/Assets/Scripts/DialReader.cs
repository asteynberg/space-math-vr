using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialReader : MonoBehaviour
{
    public GameObject dial;
    // 0 to 360
    public float initialAngle = 55;
    public float minimumValue = 0;
    public float maxiumumValue = 50;
    public bool round;
    public float value;
    private float prevValue;

    private void Start()
    {
        value = 0;
        prevValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float maxSpread = maxiumumValue - minimumValue;
        float angle = dial.transform.eulerAngles.y;
        if (angle < 0)
        {
            angle = 360 - angle;
        }
        // make sure it's positive and in the range [0, 365)
        angle = (((angle - initialAngle) % 365) + 365) % 365;
        value = angle * (maxSpread / 365) + minimumValue;
        if (round)
        {
            value = Mathf.Round(value);
        }
        gameObject.GetComponent<TextMesh>().text = value.ToString();
        if (value != prevValue)
        {
            Debug.Log("Triggering dialClick");
            EventManager.TriggerEvent(EventManager.EventName.dialClick);
        }
        prevValue = value;
    }
}
