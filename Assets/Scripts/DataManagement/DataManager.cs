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
    public float CurrentAmmo;
    public float TotalAmmo;
    public float CurrentBalance;
    public float FirstTime;

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
    }

    public override string ToString()
    {
        return $"Current Ammo: {GetCurrentAmmo()}, Total Ammo: {GetTotalAmmo()}, Current Balance: {GetCurrentBalance()}, First Time: {GetFirstTime()}";
    }
}
