using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Economy
{
    /// <summary>
    /// Represents a transaction between two wallets.
    /// </summary>
    [Serializable]
    public struct Transaction
    {
        [SerializeField]
        private string _name;
        public string Name => _name;

        /// <summary>
        /// The value of the transaction.
        /// </summary>
        [SerializeField]
        private float _value;
        public float Value => _value;

        [SerializeField]
        private TipoDeTransação _transactionType;
        public TipoDeTransação transactionType => _transactionType;

        public UnityEvent<Transaction> OnTransactionPending { get; private set; }
        public UnityEvent<Transaction> OnTransactionAccepted { get; private set; }
        public UnityEvent<Transaction> OnTransactionRefused { get; private set; }

        /// <summary>
        /// Constructor for creating a new transaction.
        /// </summary>
        /// <param name="name">The name of the transaction.</param>
        /// <param name="value">The value of the transaction.</param>
        /// <param name="sender">The wallet data of the sender.</param>
        /// <param name="receiver">The wallet data of the receiver.</param>
        /// <param name="transactionType">The type of the transaction (Physical or Digital).</param>
        public Transaction(
            float value,
            TipoDeTransação transactionType,
            string name
        )
        {
            if (value < 0)
                throw new ArgumentException("Transaction value cannot be negative.", nameof(value));

            _name = name;
            _value = value;
            _transactionType = transactionType;

            if (name == "")
                Debug.LogError("ERROR: Names for transactions are now mandatory");

            OnTransactionPending = new UnityEvent<Transaction>();
            OnTransactionAccepted = new UnityEvent<Transaction>();
            OnTransactionRefused = new UnityEvent<Transaction>();
            OnTransactionPending?.Invoke(this);
        }
    }
}
