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

    PlayerData playerData
    {
        get { return DataManager.LoadPlayerData(); }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerData.CurrentAmmoChanged.AddListener(OnCurrentAmmoChanged);
        PlayerData.TotalAmmoChanged.AddListener(OnTotalAmmoChanged);
        PlayerData.CurrentBalanceChanged.AddListener(OnCurrentBalanceChanged);
        PlayerData.FirstTimeChanged.AddListener(OnFirstTimeChanged);
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

    public void SetFirstTime(float value)
    {
        playerData.SetFirstTime(value);
    }
}
