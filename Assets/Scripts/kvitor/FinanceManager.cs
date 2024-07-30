using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinanceManager : MonoBehaviour
{
    [SerializeField]
    private float InitialBalance;

    public float CurrentBalance
    {
        get
        {
            float value = 0f;
            foreach (Transaction transaction in _transactions)
            {
                if (transaction.type == TransactionType.Credit)
                {
                    value += transaction.amount;
                }
                else if (transaction.type == TransactionType.Purchase)
                {
                    value -= transaction.amount;
                }
                else
                {
                    throw new Exception(
                        "ERRO: Tipo de transação impossível ou ainda não implementado"
                    );
                }
            }
            return value;
        }
        private set
        {
            throw new Exception(
                "ERRO: Não é possível alterar o balanço atual diretamente, adicione uma transação em vez disso"
            );
        }
    }

    private List<Transaction> _transactions = new List<Transaction>();
    public UnityEvent<EventObject> BalanceChanged;

    void Start()
    {
        if (InitialBalance != 0f)
        {
            AddCredit(InitialBalance);
        }
    }

    // Adiciona uma compra à lista de transações
    public void AddPurchase(float amount)
    {
        _transactions.Add(new Transaction(amount, TransactionType.Purchase));

        PlayerPrefs.SetFloat("CurrentBalance", CurrentBalance);
        PlayerPrefs.Save();
        EventObject _eventObject = new EventObject();
        _eventObject.floatingPoint = CurrentBalance;
        BalanceChanged?.Invoke(_eventObject);
    }

    // Adiciona um crédito à lista de transações
    public void AddCredit(float amount)
    {
        _transactions.Add(new Transaction(amount, TransactionType.Credit));

        PlayerPrefs.SetFloat("CurrentBalance", CurrentBalance);
        PlayerPrefs.Save();

        EventObject _eventObject = new EventObject();
        _eventObject.floatingPoint = CurrentBalance;
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
