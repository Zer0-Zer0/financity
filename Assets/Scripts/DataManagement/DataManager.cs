using UnityEngine;

public static class DataManager
{
    public static void SavePlayerData(PlayerData data)
    {
        Debug.Log("Saving Player data");

        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);

        Debug.Log(Application.persistentDataPath + "/playerData.json");
    }

    public static PlayerData LoadPlayerData()
    {
        Debug.Log("Loading Player data");

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
    public bool FirstTime;

    // Getter and Setter for CurrentAmmo
    public float GetCurrentAmmo()
    {
        return CurrentAmmo;
    }

    public void SetCurrentAmmo(float value)
    {
        CurrentAmmo = value;
    }

    // Getter and Setter for TotalAmmo
    public float GetTotalAmmo()
    {
        return TotalAmmo;
    }

    public void SetTotalAmmo(float value)
    {
        TotalAmmo = value;
    }

    // Getter and Setter for CurrentBalance
    public float GetCurrentBalance()
    {
        return CurrentBalance;
    }

    public void SetCurrentBalance(float value)
    {
        CurrentBalance = value;
    }

    // Getter and Setter for FirstTime
    public bool GetFirstTime()
    {
        return FirstTime;
    }

    public void SetFirstTime(bool value)
    {
        FirstTime = value;
    }

    public override string ToString()
    {
        return $"Current Ammo: {GetCurrentAmmo()}, Total Ammo: {GetTotalAmmo()}, Current Balance: {GetCurrentBalance()}, First Time: {GetFirstTime()}";
    }
}
