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
        List<Transaction> _transactions;
        public List<Transaction> Transactions
        {
            get => _transactions;
            set => _transactions = value;
        }

        [SerializeField]
        List<LoanProcessor> _loans;
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
        {
            float total = 0f;
            if (Transactions.Count == 0) return total;
            foreach (var transaction in Transactions)
                if (this == transaction.Receiver)
                    total += transaction.Value;
                else
                    total -= transaction.Value;

            //Debug.Log($"Total money in wallet: {total}");
            return total;
        }

        private float CalculateCurrentDebt()
        {
            float totalDebt = 0f;
            if (Loans.Count == 0) return totalDebt;
            foreach (var loan in Loans)
                totalDebt += loan.TotalToPay;
            return totalDebt;
        }
    }
}
