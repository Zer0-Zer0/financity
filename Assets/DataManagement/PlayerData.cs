using System;
using UnityEngine;
using UnityEngine.Events;
using Economy;
using Inventory;

[Serializable]
public class PlayerData
{
    [SerializeField]
    int _currentAmmo;

    [SerializeField]
    int _totalAmmo;

    [SerializeField]
    float _currentBalance;

    public WalletData walletData;
    public Inventory.Inventory inventory;

    [SerializeField]
    bool _firstTime;

    [SerializeField]
    bool _missionOneCompleted;

    [SerializeField]
    float _currentHealth;

    public const float MaxHealth = 10;

    public UnityEvent CurrentAmmoChanged;
    public UnityEvent TotalAmmoChanged;
    public UnityEvent CurrentBalanceChanged;
    public UnityEvent FirstTimeChanged;
    public UnityEvent MissionOneCompleted;
    public UnityEvent CurrentHealthChanged;

    // Constructor
    public PlayerData(int currentAmmo = 30, int totalAmmo = 120, float currentBalance = 1440f, bool firstTime = true, bool missionOneCompleted = false, float currentHealth = 5)
    {
        _currentAmmo = currentAmmo;
        _totalAmmo = totalAmmo;
        _currentBalance = currentBalance;
        _firstTime = firstTime;
        _missionOneCompleted = missionOneCompleted;
        _currentHealth = currentHealth;
        
        // Initialize UnityEvents
        CurrentAmmoChanged = new UnityEvent();
        TotalAmmoChanged = new UnityEvent();
        CurrentBalanceChanged = new UnityEvent();
        FirstTimeChanged = new UnityEvent();
        MissionOneCompleted = new UnityEvent();
        CurrentHealthChanged = new UnityEvent();
    }

    // Getter and Setter for CurrentAmmo
    public int GetCurrentAmmo() => _currentAmmo;

    public void SetCurrentAmmo(int value)
    {
        _currentAmmo = value;
        CurrentAmmoChanged?.Invoke();
    }

    // Getter and Setter for TotalAmmo
    public int GetTotalAmmo() => _totalAmmo;

    public void SetTotalAmmo(int value)
    {
        _totalAmmo = value;
        TotalAmmoChanged?.Invoke();
    }

    // Getter and Setter for CurrentBalance
    public float GetCurrentBalance() => _currentBalance;

    public void SetCurrentBalance(float value)
    {
        _currentBalance = value;
        CurrentBalanceChanged?.Invoke();
    }

    public float GetCurrentHealth() => _currentHealth;

    public void SetCurrentHealth(float value)
    {
        _currentHealth = Math.Min(value, MaxHealth);
        CurrentHealthChanged?.Invoke();
    }

    // Getter and Setter for FirstTime
    public bool GetFirstTime() => _firstTime;

    public void SetFirstTime(bool value)
    {
        _firstTime = value;
        FirstTimeChanged?.Invoke();
    }

    public bool GetMissionOneStatus() => _missionOneCompleted;

    public void CompleteMissionOne()
    {
        _missionOneCompleted = true;
        MissionOneCompleted?.Invoke();
    }

    public override string ToString()
    {
        return $"Current Ammo: {GetCurrentAmmo()}, Total Ammo: {GetTotalAmmo()}, Current Balance: {GetCurrentBalance()}, First Time: {GetFirstTime()}";
    }
}
