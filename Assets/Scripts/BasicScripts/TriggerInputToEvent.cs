using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerInputToEvent : TriggerToEvent
{
    public KeyCode ButtonToPress;
    public UnityEvent EventToInvoke;

    void Update()
    {
        if (Input.GetKeyUp(ButtonToPress) && _isPlayerInRange)
        {
            EventToInvoke?.Invoke();
        }
    }
}
