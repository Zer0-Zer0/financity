using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Economy
{
    public enum TransactionType
    {
        Physical,
        Digital
    }

    /// <summary>
    /// Represents a transaction between two wallets.
    /// </summary>
    [Serializable]
    public struct Transaction
    {
        [SerializeField] private string _name;
        public string Name => _name;

        /// <summary>
        /// The value of the transaction.
        /// </summary>
        [SerializeField] private float _value;
        public float Value => _value;

        /// <summary>
        /// The wallet data of the sender.
        /// </summary>
        private WalletData _sender;//Not Serializable to avoid cyclic dependences
        public WalletData Sender => _sender;

        /// <summary>
        /// The wallet data of the receiver.
        /// </summary>
        private WalletData _receiver;//Not Serializable to avoid cyclic dependences
        public WalletData Receiver => _receiver;

        /// <summary>
        /// The type of the transaction.
        /// </summary>
        [SerializeField] private TransactionType _type;
        public TransactionType Type => _type;

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
