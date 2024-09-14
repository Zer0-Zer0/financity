using UnityEngine;
using UnityEngine.Events;

public class AlienNavmeshListener : MonoBehaviour
{
    [SerializeField] GameEventListener gameEventListener;
    private void OnEnable()
    {
        if (NavmeshBaker.Instance != null)
            gameEventListener.enabled = true;
        else
        {
            gameEventListener.gameEvent.Raise(this, null);
            gameEventListener.enabled = false;
        }
    }
}
