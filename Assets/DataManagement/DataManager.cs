using System.IO;
using System.Security;
using UnityEngine;
using UnityEngine.Events;

public static class DataManager
{
    private static PlayerData _playerData;
    private static string _playerDataPath = Application.persistentDataPath + "/playerData.json";
    public static PlayerData playerData
    {
        get
        {
            if (_playerData == null)
                _playerData = LoadPlayerData();
                
            return _playerData;
        }
        set { _playerData = value; }
    }

    public static void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(_playerDataPath, json);
    }

    public static void ClearPlayerData(){
        PlayerData EmptySave = new PlayerData();
        SavePlayerData(EmptySave);
    }

    public static PlayerData LoadPlayerData()
    {
        // Load data
        if (!File.Exists(_playerDataPath))
            return new PlayerData();
        else
        {
            string json = System.IO.File.ReadAllText(_playerDataPath);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

            return loadedData;
        }
    }
}