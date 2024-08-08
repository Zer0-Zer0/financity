using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerData
{
    [SerializeField]
    int _currentAmmo = 30;

    [SerializeField]
    int _totalAmmo = 120;

    [SerializeField]
    float _currentBalance = 1440f;

    [SerializeField]
    bool _firstTime = false;

    [SerializeField]
    bool _missionOneCompleted = true;

    [SerializeField]
    float _currentHealth = 5;

    public const float MaxHealth = 10;

    public UnityEvent CurrentAmmoChanged;
    public UnityEvent TotalAmmoChanged;
    public UnityEvent CurrentBalanceChanged;
    public UnityEvent FirstTimeChanged;
    public UnityEvent MissionOneCompleted;
    public UnityEvent CurrentHealthChanged;

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
