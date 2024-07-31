using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayerData : MonoBehaviour
{
    public void SaveData()
    {
        DataManager.SavePlayerData(DataManager.playerData);
    }

    private void OnApplicationQuit()
    {
        DataManager.SavePlayerData(DataManager.playerData);
    }
}
