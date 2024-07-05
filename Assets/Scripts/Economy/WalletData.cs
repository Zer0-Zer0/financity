using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "WalletData", menuName = "WalletData", order = 0)]
public class WalletData : ScriptableObject
{
    [SerializeField] private float _initialDigitalMoney = 800f;
    [SerializeField] private float _initialPhysicalMoney = 0f;
    [SerializeField] private float _initialDebt = 800f;
    [SerializeField] private float _initialMaxDebt = 800f;

    private float _currentDigitalMoney;
    public float CurrentDigitalMoney
    {
        get
        {
            return _currentDigitalMoney;
        }
        set
        {
            if (value < 0f)
            {
                throw new Exception("Attempted to spend digital money and the value in the account got negative.");
            }
            _currentDigitalMoney = value;
            OnDigitalMoneyUpdate?.Invoke(_currentDigitalMoney);
        }
    }

    private float _currentPhysicalMoney;
    public float CurrentPhysicalMoney
    {
        get
        {
            return _currentPhysicalMoney;
        }
        set
        {
            if (value < 0f)
            {
                throw new Exception("Attempted to spend physical money and the value in the account got negative.");
            }
            _currentPhysicalMoney = value;
            OnPhysicalMoneyUpdate?.Invoke(_currentPhysicalMoney);
        }
    }

    private float _currentMaxDebt;
    public float CurrentMaxDebt
    {
        get
        {
            return _currentMaxDebt;
        }
        set
        {
            if (value < 0f)
            {
                throw new Exception("Attempted to input a negative max debt value.");
            }
            _currentMaxDebt = value;
            OnMaxDebtUpdate?.Invoke(_currentMaxDebt);
        }
    }

    private float _currentDebt;
    public float CurrentDebt
    {
        get
        {
            return _currentDebt;
        }
        set
        {
            if (value < 0f)
            {
                throw new Exception("Attempted to input a negative debt value.");
            }
            else if (value > CurrentMaxDebt)
            {
                throw new Exception("Attempted to input a bigger than allowed debt value.");
            }
            _currentDebt = value;
            OnDebtUpdate?.Invoke(_currentDebt);
        }
    }

    public UnityEvent<float> OnDigitalMoneyUpdate;
    public UnityEvent<float> OnPhysicalMoneyUpdate;
    public UnityEvent<float> OnDebtUpdate;
    public UnityEvent<float> OnMaxDebtUpdate;

    /// <summary>
    /// Initializes the wallet with initial values, Awake() is better than Start() for this.
    /// </summary>
    void Awake()
    {
        CurrentDigitalMoney = _initialDigitalMoney;
        CurrentPhysicalMoney = _initialPhysicalMoney;
        CurrentMaxDebt = _initialMaxDebt;
        CurrentDebt = _initialDebt;
    }
}