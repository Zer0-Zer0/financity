using UnityEngine;
using UnityEngine.Events;

public class AlienNavmeshListener : MonoBehaviour
{
    [SerializeField] GameEventListener gameEventListener;
    private void Start()
    {
        if (NavmeshBaker.Instance != null && NavmeshBaker.Instance.IsBaked == true)
        {
            Debug.Log($"{gameObject.name}: Enabling alien due to baked navmesh");
            EnableAlien();
        }
        else if (NavmeshBaker.Instance != null)
            gameEventListener.enabled = true;
        else
        {
            Debug.Log($"{gameObject.name}: Enabling alien due to no navmesh listener");
            EnableAlien();
        }
    }

    void EnableAlien()
    {
        gameEventListener.gameEvent.Raise(this, null);
    }
}
