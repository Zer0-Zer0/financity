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

    public float DesiredValue;

    public float CurrentAmmo
    {
        get { return playerData.GetCurrentAmmo(); }
    }

    public float TotalAmmo
    {
        get { return playerData.GetTotalAmmo(); }
    }

    public float CurrentBalance
    {
        get { return playerData.GetCurrentBalance(); }
    }

    public float FirstTime
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
    }

    public void OnCurrentAmmoChanged()
    {
        if (CurrentAmmo == DesiredValue)
        {
            CurrentAmmoChanged?.Invoke();
        }
    }

    public void OnTotalAmmoChanged()
    {
        if (TotalAmmo == DesiredValue)
        {
            TotalAmmoChanged?.Invoke();
        }
    }

    public void OnCurrentBalanceChanged()
    {
        if (CurrentBalance == DesiredValue)
        {
            CurrentBalanceChanged?.Invoke();
        }
    }

    public void OnFirstTimeChanged()
    {
        if (FirstTime == DesiredValue)
        {
            FirstTimeChanged?.Invoke();
        }
    }

    public void SetCurrentAmmo(float value)
    {
        playerData.SetCurrentAmmo(value);
    }

    public void SetTotalAmmo(float value)
    {
        playerData.SetTotalAmmo(value);
    }

    public void SetCurrentBalance(float value)
    {
        playerData.SetCurrentBalance(value);
    }

    public void AddToCurrentBalance(float value)
    {
        playerData.SetCurrentBalance(CurrentBalance + value);
    }

    public void SetFirstTime(float value)
    {
        playerData.SetFirstTime(value);
    }

    public void SaveData()
    {
        DataManager.SavePlayerData(DataManager.playerData);
    }
}
