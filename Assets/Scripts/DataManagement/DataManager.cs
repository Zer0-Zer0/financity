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
    private float currentAmmo;
    private float totalAmmo;
    private float currentBalance;
    private bool firstTime;

    // Getter and Setter for CurrentAmmo
    public float GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public void SetCurrentAmmo(float value)
    {
        currentAmmo = value;
    }

    // Getter and Setter for TotalAmmo
    public float GetTotalAmmo()
    {
        return totalAmmo;
    }

    public void SetTotalAmmo(float value)
    {
        totalAmmo = value;
    }

    // Getter and Setter for CurrentBalance
    public float GetCurrentBalance()
    {
        return currentBalance;
    }

    public void SetCurrentBalance(float value)
    {
        currentBalance = value;
    }

    // Getter and Setter for FirstTime
    public bool GetFirstTime()
    {
        return firstTime;
    }

    public void SetFirstTime(bool value)
    {
        firstTime = value;
    }

    public override string ToString()
    {
        return $"Current Ammo: {GetCurrentAmmo()}, Total Ammo: {GetTotalAmmo()}, Current Balance: {GetCurrentBalance()}, First Time: {GetFirstTime()}";
    }
}
