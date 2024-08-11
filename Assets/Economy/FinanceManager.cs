using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinanceManager : MonoBehaviour
{
    private static float _currentBalance
    {
        get { return DataManager.playerData.GetCurrentBalance(); }
    }

    // Adiciona uma compra à lista de transações
    public static void AddPurchase(float amount)
    {
        DataManager.playerData.RemoveFromCurrentBalance(amount);
    }

    // Adiciona um crédito à lista de transações
    public static void AddCredit(float amount)
    {
        DataManager.playerData.AddToCurrentBalance(amount);
    }
}
