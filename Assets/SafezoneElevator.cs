using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SafezoneElevator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private UnityEvent ElevatorChegou;

    [SerializeField]
    private Transform Player;

    public void EmitirElevadorChegou()
    {
        ElevatorChegou?.Invoke();

        if (Player != null)
        {
            Player.SetParent(null);
        }
    }
}
