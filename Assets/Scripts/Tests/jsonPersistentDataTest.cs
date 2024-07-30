using UnityEngine;

public class jsonPersistentDataTest : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public float CurrentAmmo;
        public float TotalAmmo;
        public float CurrentBalance;
        public bool FirstTime;

        public override string ToString()
        {
            return $"Current Ammo: {CurrentAmmo}, Total Ammo: {TotalAmmo}, Current Balance: {CurrentBalance}, First Time: {FirstTime}";
        }
    }

    void Start()
    {
        SaveJSONData();
        LoadJSONData();
    }

    void SaveJSONData()
    {
        Debug.Log("Saving JSON data");
        // Save data
        PlayerData data = new PlayerData
        {
            CurrentAmmo = 30f,
            TotalAmmo = 120f,
            CurrentBalance = 800f,
            FirstTime = true
        };
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
        Debug.Log(Application.persistentDataPath + "/playerData.json");
    }

    void LoadJSONData()
    {
        Debug.Log("Loading JSON data");
        // Load data
        string json = System.IO.File.ReadAllText(
            Application.persistentDataPath + "/playerData.json"
        );
        PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

        Debug.Log(loadedData);
    }
}
