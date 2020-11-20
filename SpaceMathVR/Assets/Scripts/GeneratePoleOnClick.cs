using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneratePoleOnClick : MonoBehaviour
{
    public const string makePoleButtonClickEventName = "pole_button_click";
    private UnityAction clickListener;
    public DialReader poleDialReader;
    public Material poleColor;
    public Transform poleSpawn;
    [HideInInspector]
    public GameObject pole = null;

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
        if (pole)
        {
            // get rid of the old pole
            Destroy(pole);
        }
        // create a new pole
        float poleLength = poleDialReader.value * 0.1f - 0.05f;
        pole = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        pole.GetComponent<MeshRenderer>().material = poleColor;
        pole.transform.SetPositionAndRotation(poleSpawn.position, poleSpawn.rotation);
        pole.transform.localScale = new Vector3(0.05f, poleLength, 0.05f);
        Interactable interactable = pole.AddComponent<Interactable>() as Interactable;
        interactable.interactionType = Interactable.InteractionType.grab;
        Rigidbody rigidbody = pole.AddComponent<Rigidbody>() as Rigidbody;
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
        CapsuleCollider collider = pole.GetComponent<CapsuleCollider>();
        collider.isTrigger = true;
        Debug.Log("POLE CREATED!");
    }
}
