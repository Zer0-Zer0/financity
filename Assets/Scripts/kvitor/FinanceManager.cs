using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinanceManager : MonoBehaviour
{
    private float currentBalance = 0f;
    private List<Transaction> transactions = new List<Transaction>();

    public float CurrentBalance
    {
        get { return currentBalance; }
    }

    public UnityEvent<EventObject> BalanceChanged;

    void Start()
    {
        EventObject _eventObject = new EventObject();
        _eventObject.floatingPoint = GetCurrentBalance();
        BalanceChanged?.Invoke(_eventObject);
    }

    // Adiciona uma compra à lista de transações
    public void AddPurchase(float amount)
    {
        currentBalance += amount;
        transactions.Add(new Transaction(amount, TransactionType.Purchase));
        PlayerPrefs.SetFloat("currentBalance", currentBalance);
        PlayerPrefs.Save();
        EventObject _eventObject = new EventObject();
        _eventObject.floatingPoint = GetCurrentBalance();
        BalanceChanged?.Invoke(_eventObject);
    }

    // Adiciona um crédito à lista de transações
    public void AddCredit(float amount)
    {
        currentBalance -= amount;
        transactions.Add(new Transaction(amount, TransactionType.Credit));
        PlayerPrefs.SetFloat("currentBalance", currentBalance);
        PlayerPrefs.Save();
        EventObject _eventObject = new EventObject();
        _eventObject.floatingPoint = GetCurrentBalance();
        BalanceChanged?.Invoke(_eventObject);
    }

    // Retorna o saldo atual considerando todas as transações até a data atual
    public float GetCurrentBalance()
    {
        return PlayerPrefs.GetFloat("currentBalance", CurrentBalance);
    }

    // Estrutura para representar uma transação
    private struct Transaction
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
    private enum TransactionType
    {
        Purchase,
        Credit
    }
}
