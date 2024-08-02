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
            {
                _playerData = LoadPlayerData();
            }
            return _playerData;
        }
        set { _playerData = value; }
    }

    public static void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(_playerDataPath, json);
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

[System.Serializable]
public class PlayerData
{
    [SerializeField]
    int _currentAmmo = 30;

    [SerializeField]
    int _totalAmmo = 120;

    [SerializeField]
    float _currentBalance = 1440f;

    [SerializeField]
    bool _firstTime = true;

    [SerializeField]
    bool _missionOneCompleted = false;

    public UnityEvent CurrentAmmoChanged;
    public UnityEvent TotalAmmoChanged;
    public UnityEvent CurrentBalanceChanged;
    public UnityEvent FirstTimeChanged;
    public UnityEvent MissionOneCompleted;

    // Getter and Setter for CurrentAmmo
    public int GetCurrentAmmo()
    {
        return _currentAmmo;
    }

    public void SetCurrentAmmo(int value)
    {
        _currentAmmo = value;
        CurrentAmmoChanged?.Invoke();
    }

    // Getter and Setter for TotalAmmo
    public int GetTotalAmmo()
    {
        return _totalAmmo;
    }

    public void SetTotalAmmo(int value)
    {
        _totalAmmo = value;
        TotalAmmoChanged?.Invoke();
    }

    // Getter and Setter for CurrentBalance
    public float GetCurrentBalance()
    {
        return _currentBalance;
    }

    public void SetCurrentBalance(float value)
    {
        _currentBalance = value;
        CurrentBalanceChanged?.Invoke();
    }

    // Getter and Setter for FirstTime
    public bool GetFirstTime()
    {
        return _firstTime;
    }

    public void SetFirstTime(bool value)
    {
        _firstTime = value;
        FirstTimeChanged?.Invoke();
    }

    public bool GetMissionOneStatus()
    {
        return _missionOneCompleted;
    }

    public void CompleteMissionOne()
    {
        _missionOneCompleted = true;
        MissionOneCompleted?.Invoke();
    }

    public override string ToString()
    {
        return $"Current Ammo: {GetCurrentAmmo()}, Total Ammo: {GetTotalAmmo()}, Current Balance: {GetCurrentBalance()}, First Time: {GetFirstTime()}";
    }
}
