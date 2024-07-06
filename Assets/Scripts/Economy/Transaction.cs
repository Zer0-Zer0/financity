using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Transaction
{
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

    public enum Type
    {
        Physical,
        Digital
    }

    public UnityEvent<Transaction> OnTransactionPending;
    public UnityEvent<Transaction> OnTransferAccepted;
    public UnityEvent<Transaction> OnTransferRefused;

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