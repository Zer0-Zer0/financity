using System.Collections;
using System.Collections.Generic;
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
        File.WriteAllText(_playerDataPath, json);
        playerData = data;
        Debug.Log($"Save feito.\n{data}");
    }

    public static void ClearPlayerData()
    {
        PlayerData EmptySave = new PlayerData();
        playerData = EmptySave;
        SavePlayerData(EmptySave);
        Debug.Log($"Save varrido.\n{playerData}");
    }

    public static PlayerData LoadPlayerData()
    {
        Debug.Log(_playerDataPath);
        if (!File.Exists(_playerDataPath))
            return new PlayerData();

        string json = File.ReadAllText(_playerDataPath);
        PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("JSON data is empty or null.");
            return new PlayerData();
        }
        if (loadedData.GetCurrentHealth() <= 0)
        {
            Debug.Log("Creating new player data due to not having one in the saves.");
            return new PlayerData();
        }
        else
        {
            Debug.Log($"Loaded save.\n{loadedData}");
            return loadedData;
        }
    }
}