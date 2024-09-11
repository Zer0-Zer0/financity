using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Economy
{
    [Serializable]
    public class WalletData
    {
        [SerializeField]
        float _currentMaxDebt = 1500f;

        [SerializeField]
        List<Transaction> _transactions = new List<Transaction>();
        public List<Transaction> Transactions
        {
            get => _transactions;
            set => _transactions = value;
        }

        [SerializeField]
        List<LoanProcessor> _loans = new List<LoanProcessor>();
        public List<LoanProcessor> Loans
        {
            get => _loans;
            set => _loans = value;
        }

        public GameEvent UnableToPay;

        public float CurrentMoney => CalculateCurrentMoney();
        public float CurrentDebt => CalculateCurrentDebt();
        public float CurrentMaxDebt => _currentMaxDebt;

        private float CalculateCurrentMoney()
        {/*
            if (Transactions == null)
            {
                Debug.Log("Transactions is null.");
                return 0f;
            }
            else if (Transactions.Count == 0)
            {
                Debug.Log("Transactions is empty.");
                return 0f;
            }//*/

            float total = 0f;

            foreach (Transaction transaction in Transactions)
            {
                if (this == transaction.Receiver)
                    total += transaction.Value;
                else if (this == transaction.Sender)
                    total -= transaction.Value;
            }

            return total;
        }

        private float CalculateCurrentDebt()
        {
            float totalDebt = 0f;
           // if (Loans.Count == 0) return totalDebt;
            foreach (var loan in Loans)
                totalDebt += loan.TotalToPay;
            return totalDebt;
        }
    }
}
