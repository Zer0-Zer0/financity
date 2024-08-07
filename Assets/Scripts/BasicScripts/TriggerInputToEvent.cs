using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerInputToEvent : MonoBehaviour
{
    public KeyCode ButtonToPress;
    public UnityEvent EventToInvoke;
    public UnityEvent PlayerInTrigger;
    public UnityEvent PlayerOutOfTrigger;
    private bool _isPlayerInRange;

    void Update()
    {
        if (Input.GetKey(ButtonToPress) && _isPlayerInRange)
        {
            EventToInvoke?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInTrigger?.Invoke();
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInTrigger?.Invoke();
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerOutOfTrigger?.Invoke();
            _isPlayerInRange = false;
        }
    }
}
