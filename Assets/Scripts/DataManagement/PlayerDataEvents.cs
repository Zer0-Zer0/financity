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
        get { return playerData.CurrentAmmo; }
    }

    public float TotalAmmo
    {
        get { return playerData.TotalAmmo; }
    }

    public float CurrentBalance
    {
        get { return playerData.CurrentBalance; }
    }

    public float FirstTime
    {
        get { return playerData.FirstTime; }
    }

    PlayerData playerData
    {
        get { return DataManager.LoadPlayerData(); }
    }

    // Start is called before the first frame update
    void Start()
    {
        DataManager.playerData.CurrentAmmoChanged.AddListener(OnCurrentAmmoChanged);
        DataManager.playerData.TotalAmmoChanged.AddListener(OnTotalAmmoChanged);
        DataManager.playerData.CurrentBalanceChanged.AddListener(OnCurrentBalanceChanged);
        DataManager.playerData.FirstTimeChanged.AddListener(OnFirstTimeChanged);
    }

    public void OnCurrentAmmoChanged()
    {
        if (playerData.CurrentAmmo == DesiredValue)
        {
            CurrentAmmoChanged?.Invoke();
        }
    }

    public void OnTotalAmmoChanged()
    {
        if (playerData.TotalAmmo == DesiredValue)
        {
            TotalAmmoChanged?.Invoke();
        }
    }

    public void OnCurrentBalanceChanged()
    {
        if (playerData.CurrentBalance == DesiredValue)
        {
            CurrentBalanceChanged?.Invoke();
        }
    }

    public void OnFirstTimeChanged()
    {
        if (playerData.FirstTime == DesiredValue)
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
        playerData.CurrentBalance+= value;
    }

    public void SetFirstTime(float value)
    {
        playerData.SetFirstTime(value);
    }
}
