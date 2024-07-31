using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayerData : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    // Start is called before the first frame update
    void Awake()
    {
        DataManager.playerData = playerData;
        DataManager.SavePlayerData(DataManager.playerData);

        Debug.Log(DataManager.playerData);
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
