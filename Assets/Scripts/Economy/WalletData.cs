using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages digital and physical money, debt, and debt limits.
/// </summary>
[CreateAssetMenu(fileName = "WalletData", menuName = "WalletData", order = 0)]
public class WalletData : ScriptableObject
{
    /// <summary>
    /// Initial value for digital money.
    /// </summary>
    [SerializeField] float _initialDigitalMoney = 800f;

    /// <summary>
    /// Initial value for physical money.
    /// </summary>
    [SerializeField] float _initialPhysicalMoney = 0f;

    /// <summary>
    /// Initial value for debt.
    /// </summary>
    [SerializeField] float _initialDebt = 800f;

    /// <summary>
    /// Initial value for maximum debt.
    /// </summary>
    [SerializeField] float _initialMaxDebt = 800f;

    float _currentDigitalMoney;

    /// <summary>
    /// Gets or sets the current digital money.
    /// </summary>
    public float CurrentDigitalMoney
    {
        get
        {
            return _currentDigitalMoney;
        }
        set
        {
            try
            {
                if (value < 0f)
                {
                    throw new Exception("Attempted to spend digital money and the value in the account got negative.");
                }
                _currentDigitalMoney = value;
                OnDigitalMoneyUpdate.Invoke(_currentDigitalMoney);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error changing digital money value: " + ex.Message);
            }
        }
    }

    float _currentPhysicalMoney;

    /// <summary>
    /// Gets or sets the current physical money.
    /// </summary>
    public float CurrentPhysicalMoney
    {
        get
        {
            return _currentPhysicalMoney;
        }
        set
        {
            try
            {
                if (value < 0f)
                {
                    throw new Exception("Attempted to spend physical money and the value in the account got negative.");
                }
                _currentPhysicalMoney = value;
                OnPhysicalMoneyUpdate.Invoke(_currentPhysicalMoney);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error changing physical money value: " + ex.Message);
            }
        }
    }

    float _currentMaxDebt;

    /// <summary>
    /// Gets or sets the current maximum debt.
    /// </summary>
    public float CurrentMaxDebt
    {
        get
        {
            return _currentMaxDebt;
        }
        set
        {
            try
            {
                if (value < 0f)
                {
                    throw new Exception("Attempted to input a negative max debt value.");
                }
                _currentMaxDebt = value;
                OnMaxDebtUpdate.Invoke(_currentMaxDebt);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error changing max debt value: " + ex.Message);
            }
        }
    }

    float _currentDebt;

    /// <summary>
    /// Gets or sets the current debt.
    /// </summary>
    public float CurrentDebt
    {
        get
        {
            return _currentDebt;
        }
        set
        {
            try
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
                OnDebtUpdate.Invoke(_currentDebt);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error changing debt value: " + ex.Message);
            }
        }
    }

    public UnityEvent<float> OnDigitalMoneyUpdate;
    public UnityEvent<float> OnPhysicalMoneyUpdate;
    public UnityEvent<float> OnDebtUpdate;
    public UnityEvent<float> OnMaxDebtUpdate;

    /// <summary>
    /// Initializes the wallet with initial values.
    /// </summary>
    void Start()
    {
        CurrentDigitalMoney = _initialDigitalMoney;
        CurrentPhysicalMoney = _initialPhysicalMoney;
        CurrentMaxDebt = _initialMaxDebt;
        CurrentDebt = _initialDebt;
    }
}