using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayerData : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            DataManager.playerData = DataManager.LoadPlayerData();
        }
        catch
        {
            DataManager.playerData = playerData;
        }
    }

    public void SaveData()
    {
        DataManager.SavePlayerData(DataManager.playerData);
    }

    private void OnApplicationQuit()
    {
        DataManager.SavePlayerData(DataManager.playerData);
    }
}
