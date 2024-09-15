using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using Economy;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDataEvents : MonoBehaviour
{
    #region Unity Events
    public UnityEvent CurrentAmmoChanged;
    public UnityEvent TotalAmmoChanged;
    public UnityEvent CurrentBalanceChanged;
    public UnityEvent FirstTimeChanged;
    public UnityEvent CurrentHealthChanged;
    public UnityEvent MissionOneCompleted;
    public UnityEvent MissionTwoCompleted;
    public UnityEvent IfIsFirstTime;
    public UnityEvent IfNotFirstTime;
    #endregion

    #region Properties
    private PlayerData playerData => DataManager.playerData;

    private int CurrentAmmo => playerData.GetCurrentAmmo();
    private int TotalAmmo => playerData.GetTotalAmmo();
    private float CurrentBalance => playerData.GetCurrentBalance();
    private float CurrentHealth => playerData.GetCurrentHealth();
    private bool FirstTime => playerData.GetFirstTime();
    #endregion

    #region Unity Methods
    void Start()
    {
        Debug.Log($"{gameObject.name}: has PlayerDataEvents");
        SubscribeEvents();
        FirstTimeChecker();

        if (playerData.GetMissionOneStatus())
            MissionOneCompleted?.Invoke();
        if (playerData.GetMissionTwoStatus())
            MissionTwoCompleted?.Invoke();

    }
    #endregion

    #region Event Handlers
    void InvokeEvent(UnityEvent unityEvent) => unityEvent?.Invoke();

    void OnCurrentAmmoChanged() => InvokeEvent(CurrentAmmoChanged);

    void OnTotalAmmoChanged() => InvokeEvent(TotalAmmoChanged);

    void OnCurrentBalanceChanged() => InvokeEvent(CurrentBalanceChanged);

    void OnCurrentHealthChanged() => InvokeEvent(CurrentHealthChanged);

    void OnFirstTimeChanged() => InvokeEvent(FirstTimeChanged);

    void OnMissionOneCompleted() => InvokeEvent(MissionOneCompleted);
    void OnMissionTwoCompleted() => InvokeEvent(MissionTwoCompleted);

    void SubscribeEvents()
    {
        playerData.CurrentAmmoChanged.AddListener(OnCurrentAmmoChanged);
        playerData.TotalAmmoChanged.AddListener(OnTotalAmmoChanged);
        playerData.CurrentBalanceChanged.AddListener(OnCurrentBalanceChanged);
        playerData.CurrentHealthChanged.AddListener(OnCurrentHealthChanged);
        playerData.FirstTimeChanged.AddListener(OnFirstTimeChanged);
        playerData.MissionOneCompleted.AddListener(OnMissionOneCompleted);
        playerData.MissionTwoCompleted.AddListener(OnMissionTwoCompleted);
    }

    void FirstTimeChecker()
    {
        if (FirstTime)
            IfIsFirstTime?.Invoke();
        else
            IfNotFirstTime?.Invoke();
    }
    #endregion

    #region Player Data Manipulation
    public void SetCurrentAmmo(int value) => playerData.SetCurrentAmmo(value);

    public void AddToCurrentAmmo(int value) => SetCurrentAmmo(CurrentAmmo + value);

    public void SetTotalAmmo(int value) => playerData.SetTotalAmmo(value);

    public void AddToTotalAmmo(int value) => SetTotalAmmo(TotalAmmo + value);

    public void AddToTotalAmmo(Component sender, object data)
    {
        if (data is InventoryItem item)
            SetTotalAmmo(TotalAmmo + (int)item.data);
    }

    public void AddTransaction(TransactionSO transaction) =>
        playerData.AddTransaction(transaction);

    public void AddTransaction(Transaction transaction) =>
        playerData.AddTransaction(transaction);

    public void AddToCurrentBalance(float value) =>
        playerData.AddToCurrentBalance(value);

    public void RemoveFromCurrentBalance(float value) =>
        playerData.RemoveFromCurrentBalance(value);

    public void SetCurrentHealth(float value) => playerData.SetCurrentHealth(value);

    public void AddToCurrentHealth(float value) => SetCurrentHealth(CurrentHealth + value);

    public void AddToCurrentHealth(Component sender, object data)
    {
        if (data is InventoryItem item)
            SetCurrentHealth(CurrentHealth + (int)item.data);
    }

    public void RemoveFromCurrentHealth(float value) => SetCurrentHealth(CurrentHealth - value);

    public void SetMaxHealth(float value) => playerData.SetMaxHealth(value);

    public void SetFirstTime(bool value) => playerData.SetFirstTime(value);

    public void CompleteMissionOne() => playerData.CompleteMissionOne();
    public void CompleteMissionTwo() => playerData.CompleteMissionTwo();
    #endregion

    #region Getters
    public int GetCurrentAmmo() => CurrentAmmo;

    public int GetTotalAmmo() => TotalAmmo;

    public float GetCurrentBalance() => CurrentBalance;

    public bool GetFirstTime() => FirstTime;

    public float GetCurrentHealth() => CurrentHealth;
    #endregion

    #region Data Management
    public void SaveData() => DataManager.SavePlayerData(playerData);
    #endregion
}
