using UnityEngine;

public class GameEventEmitter : MonoBehaviour
{
    [SerializeField] GameEvent Event;
    public void Invoke() => Event.Raise(this, null);
}