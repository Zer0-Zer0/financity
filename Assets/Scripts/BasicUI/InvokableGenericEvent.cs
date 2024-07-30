using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokableGenericEvent : MonoBehaviour
{
    [SerializeField] private string EventName;
    [SerializeField] private string EventDescription;
    [SerializeField] private UnityEvent EventInvoked;
    public void InvokeEvent(){
        EventInvoked?.Invoke();
    }
}
