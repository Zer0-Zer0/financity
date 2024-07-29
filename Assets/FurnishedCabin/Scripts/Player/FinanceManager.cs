/*using UnityEngine;
using System.Collections.Generic;

public class FinanceManager : MonoBehaviour
{
    private float currentBalance = 0f;
    private List<Transaction> transactions = new List<Transaction>();

    public float CurrentBalance
    {
        get { return currentBalance; }
    }

    // Adiciona uma compra à lista de transações
    public void AddPurchase(float amount)
    {
        currentBalance += amount;
        transactions.Add(new Transaction(amount, TransactionType.Purchase));
    }

    // Adiciona um crédito à lista de transações
    public void AddCredit(float amount)
    {
        currentBalance -= amount;
        transactions.Add(new Transaction(amount, TransactionType.Credit));
    }

    // Retorna o saldo atual considerando todas as transações até a data atual
    public float GetCurrentBalance()
    {
        float balance = 0f;
        foreach (Transaction transaction in transactions)
        {
            balance += transaction.amount;
        }
        return balance;
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
*/