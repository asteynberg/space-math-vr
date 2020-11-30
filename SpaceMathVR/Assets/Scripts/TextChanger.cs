using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextChanger : MonoBehaviour
{

    [TextArea(15, 20)]
    public string challengeText = "The closet is 12 ft high and\n5 ft wide.Please create a pole\nwith a length that will fit\nperfectly along the back\nwall of the closet.\n\nTurn the dial to set the length of\nthe pole, and press the green square\nbutton to create the pole.";

    [TextArea(15, 20)]
    public string victoryText = "Congratulations!\nPress the \"Next Assignment\"\nbutton to continue...";

    [TextArea(15, 20)]
    public string endText = "To be continued...";

    private UnityAction challengeAction, victoryAction, endAction;
    void Awake()
    {
        challengeAction = new UnityAction(challengeTextChanger);
        victoryAction = new UnityAction(victoryTextChanger);
        endAction = new UnityAction(endTextChanger);
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

    private void victoryTextChanger()
    {
        gameObject.GetComponent<TextMesh>().text = victoryText;
    }

    private void challengeTextChanger()
    {
        gameObject.GetComponent<TextMesh>().text = challengeText;
    }

    private void endTextChanger()
    {
        gameObject.GetComponent<TextMesh>().text = endText;
    }
}
