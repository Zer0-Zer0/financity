using UnityEngine;
using UnityEngine.Events;

public static class DataManager
{
    private static PlayerData _playerData;
    public static PlayerData playerData
    {
        get
        {
            if (_playerData == null)
            {
                _playerData = new PlayerData();
            }
            return _playerData;
        }
        set { _playerData = value; }
    }

    public static void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
    }

    public static PlayerData LoadPlayerData()
    {
        // Load data
        string json = System.IO.File.ReadAllText(
            Application.persistentDataPath + "/playerData.json"
        );
        PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

        Debug.Log(loadedData);

        return loadedData;
    }
}

[System.Serializable]
public class PlayerData
{
    [SerializeField]
    float _currentAmmo = 30f;

    [SerializeField]
    float _totalAmmo = 120f;

    [SerializeField]
    float _currentBalance = 1440f;

    [SerializeField]
    float _firstTime = 1f;

    public UnityEvent CurrentAmmoChanged;
    public UnityEvent TotalAmmoChanged;
    public UnityEvent CurrentBalanceChanged;
    public UnityEvent FirstTimeChanged;

    // Getter and Setter for CurrentAmmo
    public float GetCurrentAmmo()
    {
        return _currentAmmo;
    }

    public void SetCurrentAmmo(float value)
    {
        _currentAmmo = value;
        CurrentAmmoChanged?.Invoke();
    }

    // Getter and Setter for TotalAmmo
    public float GetTotalAmmo()
    {
        return _totalAmmo;
    }

    public void SetTotalAmmo(float value)
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
    public float GetFirstTime()
    {
        return _firstTime;
    }

    public void SetFirstTime(float value)
    {
        _firstTime = value;
        FirstTimeChanged?.Invoke();
    }

    public override string ToString()
    {
        return $"Current Ammo: {GetCurrentAmmo()}, Total Ammo: {GetTotalAmmo()}, Current Balance: {GetCurrentBalance()}, First Time: {GetFirstTime()}";
    }
}
