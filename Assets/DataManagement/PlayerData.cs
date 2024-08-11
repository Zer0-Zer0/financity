using System;
using Economy;
using InventorySystem;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayerData
{
    [SerializeField]
    int _currentAmmo;

    [SerializeField]
    int _totalAmmo;

    [SerializeField]
    WalletData _walletData;

    [SerializeField]
    Inventory _inventory;

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
    public PlayerData(
        int currentAmmo = 30,
        int totalAmmo = 120,
        WalletData wallet = null,
        Inventory inventory = null,
        float currentHealth = 5
    )
    {
        _currentAmmo = currentAmmo;
        _totalAmmo = totalAmmo;

        if (wallet == null)
            _walletData = new WalletData();
        else
            _walletData = wallet;

        if (_inventory == null)
            _inventory = new Inventory(12);
        else
            _inventory = inventory;

        _firstTime = true;
        _missionOneCompleted = false;
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

    public float GetCurrentBalance() => _walletData.CurrentDigitalMoney;

    public void AddToCurrentBalance(float value)
    {
        if (value <= 0)
            throw new ArgumentException("Cannot add a non-positive amount to the balance.");

        Transaction transaction = new Transaction(value, TransactionType.Digital, _walletData);
        _walletData.Transactions.Add(transaction);
        CurrentBalanceChanged?.Invoke();
    }

    public void RemoveFromCurrentBalance(float value)
    {
        if (value <= 0)
            throw new ArgumentException("Cannot remove a non-positive amount from the balance.");

        if (value > GetCurrentBalance())
            throw new InvalidOperationException("Cannot remove more than the current balance.");

        Transaction transaction = new Transaction(-value, TransactionType.Digital, _walletData);
        _walletData.Transactions.Add(transaction);
        CurrentBalanceChanged?.Invoke();
    }

    public float GetCurrentHealth() => _currentHealth;

    public void SetCurrentHealth(float value)
    {
        _currentHealth = Math.Min(value, MaxHealth);
        CurrentHealthChanged?.Invoke();
    }

    public Inventory GetCurrentInventory() => _inventory;

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
