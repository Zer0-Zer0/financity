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
            Debug.Log($"{gameObject.name}: Enabling alien due to no navmesh listener");
            gameEventListener.gameEvent.Raise(this, null);
        }
    }
}
