//Fonte de inspiração: https://youtu.be/7_dyDmF0Ktw
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomUnityEvent : UnityEvent<Component, object> {}

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;

    public CustomUnityEvent response;
    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(Component sender, object data)
    {
        response.Invoke(sender, data);
    }
}
