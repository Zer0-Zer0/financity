using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnStart : MonoBehaviour
{
    [SerializeField] UnityEvent eventStart = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        eventStart?.Invoke();
    }
}
