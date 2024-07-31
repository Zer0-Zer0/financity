using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinanceManager : MonoBehaviour
{
    public UnityEvent<EventObject> BalanceChanged;
    public static FinanceManager financeManager;

    public static void InvokeBalanceChanged()
    {
        EventObject _eventObject = new EventObject();
        _eventObject.floatingPoint = DataManager.playerData.CurrentBalance;
        financeManager.BalanceChanged?.Invoke(_eventObject);
    }

    public static void OnBalanceChanged(EventObject value)
    {
        Debug.Log("Balanço mudou");
        Debug.Log(DataManager.playerData.CurrentBalance);
    }

    // Adiciona uma compra à lista de transações
    public static void AddPurchase(float amount)
    {
        DataManager.playerData.CurrentBalance -= amount;
        EventObject _eventObject = new EventObject();
        _eventObject.floatingPoint = DataManager.playerData.CurrentBalance;
        financeManager.BalanceChanged?.Invoke(_eventObject);
    }

    // Adiciona um crédito à lista de transações
    public static void AddCredit(float amount)
    {
        DataManager.playerData.CurrentBalance += amount;
        EventObject _eventObject = new EventObject();
        _eventObject.floatingPoint = DataManager.playerData.CurrentBalance;
        financeManager.BalanceChanged?.Invoke(_eventObject);
    }
}
