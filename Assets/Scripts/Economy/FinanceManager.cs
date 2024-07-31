using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinanceManager : MonoBehaviour
{
    private static float _currentBalance
    {
        get { return DataManager.playerData.GetCurrentBalance(); }
        set { DataManager.playerData.SetCurrentBalance(value); }
    }

    // Adiciona uma compra à lista de transações
    public static void AddPurchase(float amount)
    {
        _currentBalance -= amount;
    }

    // Adiciona um crédito à lista de transações
    public static void AddCredit(float amount)
    {
        _currentBalance += amount;
    }
}
