using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test : MonoBehaviour
{
    public UnityEvent onTeleport;

    // Start is called before the first frame update
    void Start()
    {
        onTeleport?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
