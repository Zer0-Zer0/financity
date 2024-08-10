using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Economy
{
    /// <summary>
    /// Enum representing the type of transaction.
    /// </summary>
    public enum TransactionType
    {
        Physical,
        Digital
    }

    /// <summary>
    /// Represents a transaction between two wallets.
    /// </summary>
    [System.Serializable]
    public struct Transaction
    {
        /// <summary>
        /// The name of the transaction.
        /// </summary>
        [SerializeField] private readonly string _name;
        public string Name => _name;

        /// <summary>
        /// The value of the transaction.
        /// </summary>
        [SerializeField] private readonly float _value;
        public float Value => _value;

        /// <summary>
        /// The wallet data of the sender.
        /// </summary>
        [SerializeField] private readonly WalletData _sender;
        public WalletData Sender => _sender;

        /// <summary>
        /// The wallet data of the receiver.
        /// </summary>
        [SerializeField] private readonly WalletData _receiver;
        public WalletData Receiver => _receiver;

        /// <summary>
        /// The type of the transaction.
        /// </summary>
        [SerializeField] private readonly TransactionType _type;
        public TransactionType Type => _type;

        /// <summary>
        /// Event triggered when a transaction is pending.
        /// </summary>
        public UnityEvent<Transaction> OnTransactionPending { get; }

        /// <summary>
        /// Event triggered when a transfer is accepted.
        /// </summary>
        public UnityEvent<Transaction> OnTransactionAccepted { get; }

        /// <summary>
        /// Event triggered when a transfer is refused.
        /// </summary>
        public UnityEvent<Transaction> OnTransactionRefused { get; }

        /// <summary>
        /// Constructor for creating a new transaction.
        /// </summary>
        /// <param name="name">The name of the transaction.</param>
        /// <param name="value">The value of the transaction.</param>
        /// <param name="sender">The wallet data of the sender.</param>
        /// <param name="receiver">The wallet data of the receiver.</param>
        /// <param name="transactionType">The type of the transaction (Physical or Digital).</param>
        public Transaction(float value, TransactionType type, WalletData receiver = null, WalletData sender = null, string name = "")
        {
            _name = name;
            _value = value;
            _sender = sender;
            _receiver = receiver;
            _type = type;

            OnTransactionPending = new UnityEvent<Transaction>();
            OnTransactionAccepted = new UnityEvent<Transaction>();
            OnTransactionRefused = new UnityEvent<Transaction>();
            OnTransactionPending?.Invoke(this);
        }
    }
}