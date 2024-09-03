using System;
using Economy;
using InventorySystem;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayerData
{
    #region Fields

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

    [SerializeField]
    Gênero _gênero;

    #endregion

    #region Unity Events

    public UnityEvent CurrentAmmoChanged;
    public UnityEvent TotalAmmoChanged;
    public UnityEvent CurrentBalanceChanged;
    public UnityEvent FirstTimeChanged;
    public UnityEvent MissionOneCompleted;
    public UnityEvent CurrentHealthChanged;

    #endregion

    #region Constructor
    public PlayerData(
        int currentAmmo = 30,
        int totalAmmo = 120,
        WalletData wallet = null,
        Inventory inventory = null,
        Gênero gênero = Gênero.Neutro,
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

        _gênero = gênero;

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

    #endregion

    #region Getters and Setters
    public int GetCurrentAmmo() => _currentAmmo;

    public void SetCurrentAmmo(int value)
    {
        _currentAmmo = value;
        CurrentAmmoChanged?.Invoke();
    }

    public int GetTotalAmmo() => _totalAmmo;

    public void SetTotalAmmo(int value)
    {
        _totalAmmo = value;
        TotalAmmoChanged?.Invoke();
    }

    public float GetCurrentBalance() => _walletData.CurrentMoney;


    public void AddTransaction(TransactionSO transaction)
    {
        AddTransaction(transaction.Instance);
        CurrentBalanceChanged?.Invoke();
    }

    public void AddTransaction(Transaction transaction)
    {
        _walletData.Transactions.Add(transaction);
        CurrentBalanceChanged?.Invoke();
    }

    public void AddToCurrentBalance(float value)
    {
        if (value <= 0)
            throw new ArgumentException("Cannot add a non-positive amount to the balance.");

        Debug.LogWarning("WARNING: This will be deprecated in favor of Add transaction");

        Transaction transaction = new Transaction(value, _walletData, null, "Crédito");
        _walletData.Transactions.Add(transaction);
        CurrentBalanceChanged?.Invoke();
    }

    public void RemoveFromCurrentBalance(float value)
    {
        if (value <= 0)
            throw new ArgumentException("Cannot remove a non-positive amount from the balance.");

        if (value > GetCurrentBalance())
            throw new InvalidOperationException("Cannot remove more than the current balance.");

        Debug.LogWarning("WARNING: This will be deprecated in favor of Add transaction");

        Transaction transaction = new Transaction(value, null, _walletData, "Transação");
        _walletData.Transactions.Add(transaction);
        CurrentBalanceChanged?.Invoke();
    }

    public WalletData GetCurrentWallet() => _walletData;

    public void SetCurrentWallet(WalletData value) => _walletData = value;

    public float GetCurrentHealth() => _currentHealth;

    public void SetCurrentHealth(float value)
    {
        _currentHealth = Math.Min(value, MaxHealth);
        CurrentHealthChanged?.Invoke();
    }

    public Inventory GetCurrentInventory() => _inventory;

    public void SetCurrentInventory(Inventory value) => _inventory = value;

    public bool GetFirstTime() => _firstTime;

    public void SetFirstTime(bool value)
    {
        _firstTime = value;
        FirstTimeChanged?.Invoke();
    }

    public Gênero getGender() => _gênero;

    public bool GetMissionOneStatus() => _missionOneCompleted;

    public void CompleteMissionOne()
    {
        _missionOneCompleted = true;
        MissionOneCompleted?.Invoke();
    }

    #endregion

    #region Overrides

    public override string ToString()
    {
        return $"Current Ammo: {GetCurrentAmmo()}, Total Ammo: {GetTotalAmmo()}, Current Balance: {GetCurrentBalance()}, First Time: {GetFirstTime()}";
    }

    #endregion
}
