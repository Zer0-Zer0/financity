using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaulDierChooser : MonoBehaviour
{
    [SerializeField] GameObject DefaultSaulDier;
    [SerializeField] GameObject MissionOneCompletedSaulDier;
    [SerializeField] GameObject MissionTwoGranterSaulDier;
    [SerializeField] GameObject MissionTwoCompletedSaulDier;
    void Start()
    {
        if(DataManager.playerData.Days == 1 && DataManager.playerData.GetMissionOneStatus() == false){
            DefaultSaulDier.SetActive(true);
            MissionOneCompletedSaulDier.SetActive(false);
            MissionTwoGranterSaulDier.SetActive(false);
            MissionTwoCompletedSaulDier.SetActive(false);
        }else if (DataManager.playerData.Days == 1 && DataManager.playerData.GetMissionOneStatus() == true){
            DefaultSaulDier.SetActive(false);
            MissionOneCompletedSaulDier.SetActive(true);
            MissionTwoGranterSaulDier.SetActive(false);
            MissionTwoCompletedSaulDier.SetActive(false);
        }else if (DataManager.playerData.Days == 2 && DataManager.playerData.GetMissionTwoStatus() == false){
            DefaultSaulDier.SetActive(false);
            MissionOneCompletedSaulDier.SetActive(false);
            MissionTwoGranterSaulDier.SetActive(true);
            MissionTwoCompletedSaulDier.SetActive(false);
        }else if (DataManager.playerData.Days == 2 && DataManager.playerData.GetMissionTwoStatus() == true){
            DefaultSaulDier.SetActive(false);
            MissionOneCompletedSaulDier.SetActive(false);
            MissionTwoGranterSaulDier.SetActive(false);
            MissionTwoCompletedSaulDier.SetActive(true);
        }
    }
}