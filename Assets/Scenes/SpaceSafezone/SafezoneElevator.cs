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
    private GameObject Player;

    public void EmitirElevadorChegou()
    {
        ElevatorChegou?.Invoke();

        if (Player != null)
        {
            Player.transform.SetParent(null);
        }
    }

    private void Update()
    {
        Player = TagFinder.FindObjectWithTag("Player");
    }
}
