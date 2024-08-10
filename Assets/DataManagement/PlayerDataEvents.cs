using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDataEvents : MonoBehaviour
{
    public UnityEvent CurrentAmmoChanged;
    public UnityEvent TotalAmmoChanged;
    public UnityEvent CurrentBalanceChanged;
    public UnityEvent FirstTimeChanged;
    public UnityEvent CurrentHealthChanged;
    public UnityEvent MissionOneCompleted;
    public UnityEvent IfIsFirstTime;
    public UnityEvent IfNotFirstTime;

    int CurrentAmmo
    {
        get { return playerData.GetCurrentAmmo(); }
    }

    int TotalAmmo
    {
        get { return playerData.GetTotalAmmo(); }
    }

    float CurrentBalance
    {
        get { return playerData.GetCurrentBalance(); }
    }

    float CurrentHealth
    {
        get { return playerData.GetCurrentHealth(); }
    }

    bool FirstTime
    {
        get { return playerData.GetFirstTime(); }
    }

    PlayerData playerData
    {
        get { return DataManager.playerData; }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerData.CurrentAmmoChanged.AddListener(OnCurrentAmmoChanged);
        playerData.TotalAmmoChanged.AddListener(OnTotalAmmoChanged);
        playerData.CurrentBalanceChanged.AddListener(OnCurrentBalanceChanged);
        playerData.FirstTimeChanged.AddListener(OnFirstTimeChanged);
        playerData.MissionOneCompleted.AddListener(OnMissionOneCompleted);
        if (playerData.GetFirstTime())
        {
            IfIsFirstTime?.Invoke();
        }
        else
        {
            IfNotFirstTime?.Invoke();
        }

        if (playerData.GetMissionOneStatus())
        {
            OnMissionOneCompleted();
        }
    }

    public void OnCurrentAmmoChanged()
    {
        CurrentAmmoChanged?.Invoke();
    }

    public void OnTotalAmmoChanged()
    {
        TotalAmmoChanged?.Invoke();
    }

    public void OnCurrentBalanceChanged()
    {
        CurrentBalanceChanged?.Invoke();
    }

    public void OnCurrentHealthChanged()
    {
        CurrentHealthChanged?.Invoke();
    }

    public void OnFirstTimeChanged()
    {
        FirstTimeChanged?.Invoke();
    }

    public void OnMissionOneCompleted()
    {
        MissionOneCompleted?.Invoke();
    }

    public void SetCurrentAmmo(int value)
    {
        playerData.SetCurrentAmmo(value);
    }

    public void AddToCurrentAmmo(int value)
    {
        playerData.SetCurrentAmmo(CurrentAmmo + value);
    }

    public void SetTotalAmmo(int value)
    {
        playerData.SetTotalAmmo(value);
    }

    public void AddToTotalAmmo(int value)
    {
        playerData.SetTotalAmmo(TotalAmmo + value);
    }

    public void AddToTotalAmmo(Component sender, object data)
    {
        if (data is InventoryItem item)
            playerData.SetTotalAmmo(TotalAmmo + ((int)item.data));
    }

    public void SetCurrentBalance(float value)
    {
        playerData.SetCurrentBalance(value);
    }

    public void AddToCurrentBalance(float value)
    {
        playerData.SetCurrentBalance(CurrentBalance + value);
    }

    public void RemoveFromCurrentBalance(float value)
    {
        playerData.SetCurrentBalance(CurrentBalance - value);
    }

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public void SetCurrentHealth(float value)
    {
        playerData.SetCurrentHealth(value);
    }

    public void AddToCurrentHealth(float value)
    {
        playerData.SetCurrentHealth(CurrentHealth + value);
    }

    public void AddToCurrentHealth(Component sender, object data)
    {
        if (data is InventoryItem item)
            playerData.SetCurrentHealth(TotalAmmo + ((int)item.data));
    }

    public void RemoveFromCurrentHealth(float value)
    {
        playerData.SetCurrentHealth(CurrentHealth - value);
    }

    public void SetFirstTime(bool value)
    {
        playerData.SetFirstTime(value);
    }

    public void CompleteMissionOne()
    {
        playerData.CompleteMissionOne();
    }

    public int GetCurrentAmmo()
    {
        return CurrentAmmo;
    }

    public int GetTotalAmmo()
    {
        return TotalAmmo;
    }

    public float GetCurrentBalance()
    {
        return CurrentBalance;
    }

    public bool GetFirstTime()
    {
        return FirstTime;
    }

    public void SaveData()
    {
        DataManager.SavePlayerData(DataManager.playerData);
    }
}
