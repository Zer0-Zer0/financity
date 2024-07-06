using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Represents a transaction between two wallets.
/// </summary>
[System.Serializable]
public struct Transaction
{
    /// <summary>
    /// The name of the transaction.
    /// </summary>
    [SerializeField] private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
        private set
        {
            _name = value;
        }
    }

    /// <summary>
    /// The value of the transaction.
    /// </summary>
    [SerializeField] private float _value;
    public float Value
    {
        get
        {
            return _value;
        }
        private set
        {
            _value = value;
        }
    }

    /// <summary>
    /// The wallet data of the sender.
    /// </summary>
    [SerializeField] private WalletData _sender;
    public WalletData Sender
    {
        get
        {
            return _sender;
        }
        private set
        {
            _sender = value;
        }
    }

    /// <summary>
    /// The wallet data of the receiver.
    /// </summary>
    [SerializeField] private WalletData _receiver;
    public WalletData Receiver
    {
        get
        {
            return _receiver;
        }
        private set
        {
            _receiver = value;
        }
    }

    /// <summary>
    /// The type of the transaction.
    /// </summary>
    [SerializeField] private Type _transactionType;
    public Type TransactionType
    {
        get
        {
            return _transactionType;
        }
        private set
        {
            _transactionType = value;
        }
    }

    /// <summary>
    /// Enum representing the type of transaction.
    /// </summary>
    public enum Type
    {
        Physical,
        Digital
    }

    /// <summary>
    /// Event triggered when a transaction is pending.
    /// </summary>
    public UnityEvent<Transaction> OnTransactionPending;

    /// <summary>
    /// Event triggered when a transfer is accepted.
    /// </summary>
    public UnityEvent<Transaction> OnTransferAccepted;

    /// <summary>
    /// Event triggered when a transfer is refused.
    /// </summary>
    public UnityEvent<Transaction> OnTransferRefused;

    /// <summary>
    /// Constructor for creating a new transaction.
    /// </summary>
    /// <param name="name">The name of the transaction.</param>
    /// <param name="value">The value of the transaction.</param>
    /// <param name="sender">The wallet data of the sender.</param>
    /// <param name="receiver">The wallet data of the receiver.</param>
    /// <param name="transactionType">The type of the transaction (Physical or Digital).</param>
    public Transaction(string name, float value, WalletData sender, WalletData receiver, Type transactionType)
    {
        this._name = name;
        this._value = value;
        this._sender = sender;
        this._receiver = receiver;
        this._transactionType = transactionType;

        OnTransactionPending = new UnityEvent<Transaction>();
        OnTransferAccepted = new UnityEvent<Transaction>();
        OnTransferRefused = new UnityEvent<Transaction>();
        OnTransactionPending?.Invoke(this);
    }
}
