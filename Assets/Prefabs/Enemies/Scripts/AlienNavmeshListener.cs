using UnityEngine;
using UnityEngine.Events;

public class AlienNavmeshListener : MonoBehaviour
{
    public GameEvent gameEvent;

    public CustomUnityEvent response;
    private void OnEnable() => gameEvent.RegisterListener(this);

    private void OnDisable() => gameEvent.UnregisterListener(this);

    public void OnEventRaised(Component sender, object data) => response.Invoke(sender, data);
}
