using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvent", order = 0)]
public class GameEvent : ScriptableObject {
    //Fonte de inspiração: https://youtu.be/7_dyDmF0Ktw
    public List<GameEventListener> listeners = new List<GameEventListener>();
    public void Raise()
    {
        foreach (GameEventListener listener in listeners)
        {
            listener.OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
        else
            Debug.LogWarning($"WARNING: Tried to add [{listener.gameObject.name}] to [{this.name}] when it was already added.");
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Add(listener);
        else
            Debug.LogWarning($"WARNING: Tried to remove [{listener.gameObject.name}] from [{this.name}] when it wasn't attached to begin with.");
    }
}