using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinanceManager : MonoBehaviour
{
    public UnityEvent<EventObject> BalanceChanged;

    void Start()
    {
        BalanceChanged.AddListener(OnBalanceChanged);
        DataManager.playerData.CurrentBalanceChanged.AddListener(InvokeBalanceChanged);
    }

    void InvokeBalanceChanged()
    {
        EventObject _eventObject = new EventObject();
        _eventObject.floatingPoint = DataManager.playerData.CurrentBalance;
        BalanceChanged?.Invoke(_eventObject);
    }

    void OnBalanceChanged(EventObject value)
    {
        Debug.Log("Balanço mudou");
        Debug.Log(DataManager.playerData.CurrentBalance);
    }

    // Adiciona uma compra à lista de transações
    public void AddPurchase(float amount)
    {
        DataManager.playerData.CurrentBalance -= amount;
        EventObject _eventObject = new EventObject();
        _eventObject.floatingPoint = DataManager.playerData.CurrentBalance;
        BalanceChanged?.Invoke(_eventObject);
    }

    // Adiciona um crédito à lista de transações
    public void AddCredit(float amount)
    {
        DataManager.playerData.CurrentBalance += amount;
        EventObject _eventObject = new EventObject();
        _eventObject.floatingPoint = DataManager.playerData.CurrentBalance;
        BalanceChanged?.Invoke(_eventObject);
    }

    // Estrutura para representar uma transação
    [Serializable]
    public struct Transaction
    {
        public float amount;
        public TransactionType type;

        public Transaction(float amount, TransactionType type)
        {
            this.amount = amount;
            this.type = type;
        }
    }

    // Enumeração para tipo de transação
    [Serializable]
    public enum TransactionType
    {
        Purchase,
        Credit
    }
}
