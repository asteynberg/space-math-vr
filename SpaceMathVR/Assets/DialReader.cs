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

    // Start is called before the first frame update
    void Start()
    {
        
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
        int value = (int)Mathf.Round(angle * (maxSpread / 365) + minimumValue);
        gameObject.GetComponent<TextMesh>().text = value.ToString();
    }
}
