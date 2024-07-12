using UnityEngine;
using System.Collections.Generic;

public class FinanceManagerDecoupling : MonoBehaviour
{
    private float currentBalance = 0f;
    private List<Transaction> transactions = new List<Transaction>();

    public float CurrentBalance
    {
        get { return currentBalance; }
    }
    public void AddPurchase(float amount)
    {
        currentBalance += amount;
        transactions.Add(new Transaction(amount, TransactionType.Purchase));
    }

    public void AddCredit(float amount)
    {
        currentBalance -= amount;
        transactions.Add(new Transaction(amount, TransactionType.Credit));
    }

    public float GetCurrentBalance()
    {
        float balance = 0f;
        foreach (Transaction transaction in transactions)
        {
            balance += transaction.amount;
        }
        return balance;
    }

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

    private enum TransactionType
    {
        Purchase,
        Credit
    }
}