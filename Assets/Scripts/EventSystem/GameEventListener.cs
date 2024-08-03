using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    //Fonte de inspiração: https://youtu.be/7_dyDmF0Ktw
    public GameEvent gameEvent;

    public UnityEvent response;
    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}
