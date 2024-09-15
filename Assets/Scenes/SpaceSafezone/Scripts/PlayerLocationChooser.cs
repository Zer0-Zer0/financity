using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject Elevator;
    [SerializeField] GameObject StoppedElevator;
    [SerializeField] GameObject ElevatorPlayer;
    [SerializeField] PrefabSpawner MainExitPlayer;
    [SerializeField] PrefabSpawner RoomExitPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.playerData.Days == 1 && DataManager.playerData.GetMissionOneStatus() == false)
        {
            Elevator.SetActive(true);
            ElevatorPlayer.SetActive(true);
            StoppedElevator.SetActive(false);
        }
        else if (DataManager.playerData.Days == 1 && DataManager.playerData.GetMissionOneStatus() == true)
        {
            Destroy(ElevatorPlayer);
            Destroy(Elevator);
            MainExitPlayer.SpawnPrefab();
            StoppedElevator.SetActive(true);
        }
        else
        {
            Destroy(ElevatorPlayer);
            Destroy(Elevator);
            RoomExitPlayer.SpawnPrefab();
            StoppedElevator.SetActive(true);
        }
    }
}
