using UnityEngine;
using UnityEngine.Events;

public static class DataManager
{
    public static PlayerData playerData;

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
    public float CurrentAmmo
    {
        get { return CurrentAmmo; }
        set
        {
            CurrentAmmoChanged?.Invoke();
            CurrentAmmo = value;
        }
    }
    public float TotalAmmo
    {
        get { return TotalAmmo; }
        set
        {
            TotalAmmoChanged?.Invoke();
            TotalAmmo = value;
        }
    }
    public float CurrentBalance
    {
        get { return CurrentBalance; }
        set
        {
            CurrentBalanceChanged?.Invoke();
            CurrentBalance = value;
        }
    }
    public float FirstTime
    {
        get { return FirstTime; }
        set
        {
            FirstTimeChanged?.Invoke();
            FirstTime = value;
        }
    }

    public UnityEvent CurrentAmmoChanged;
    public UnityEvent TotalAmmoChanged;
    public UnityEvent CurrentBalanceChanged;
    public UnityEvent FirstTimeChanged;

    // Getter and Setter for CurrentAmmo
    public float GetCurrentAmmo()
    {
        return CurrentAmmo;
    }

    public void SetCurrentAmmo(float value)
    {
        CurrentAmmo = value;
        CurrentAmmoChanged?.Invoke();
        DataManager.playerData = this;
    }

    // Getter and Setter for TotalAmmo
    public float GetTotalAmmo()
    {
        return TotalAmmo;
    }

    public void SetTotalAmmo(float value)
    {
        TotalAmmo = value;
        TotalAmmoChanged?.Invoke();
        DataManager.playerData = this;
    }

    // Getter and Setter for CurrentBalance
    public float GetCurrentBalance()
    {
        return CurrentBalance;
    }

    public void SetCurrentBalance(float value)
    {
        CurrentBalance = value;
        CurrentBalanceChanged?.Invoke();
        DataManager.playerData = this;
    }

    // Getter and Setter for FirstTime
    public float GetFirstTime()
    {
        return FirstTime;
    }

    public void SetFirstTime(float value)
    {
        FirstTime = value;
        FirstTimeChanged?.Invoke();
        DataManager.playerData = this;
    }

    public override string ToString()
    {
        return $"Current Ammo: {GetCurrentAmmo()}, Total Ammo: {GetTotalAmmo()}, Current Balance: {GetCurrentBalance()}, First Time: {GetFirstTime()}";
    }
}
