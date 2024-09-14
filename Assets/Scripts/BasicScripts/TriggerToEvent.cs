using UnityEngine;
using UnityEngine.Events;

public class TriggerToEvent : MonoBehaviour {
    
    public UnityEvent PlayerInTrigger;
    public UnityEvent PlayerOutOfTrigger;
    protected bool _isPlayerInRange;

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