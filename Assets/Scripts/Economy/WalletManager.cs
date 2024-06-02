using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages digital and physical money, debt, and debt limits.
/// </summary>
public class WalletManager : MonoBehaviour
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
    public float CurrentDigitalMoney{
        get{
            return _currentDigitalMoney;
        }
        set{
            try{
                OnDigitalMoneyUpdate.Invoke();
                if(value < 0f){
                    throw new Exception("Atempted to spend digital money and the value in the account got negative.");
                }
                else if(value > _currentDigitalMoney){
                    OnDigitalMoneyIncrease.Invoke();
                }
                else if (value < _currentDigitalMoney){
                    OnDigitalMoneyReduce.Invoke();
                }
                _currentDigitalMoney = value;
            } catch (UnassignedReferenceException ex) {
                Debug.LogError("Error changing digital money value: "ex.Message);
            }
        }
    }

    float _currentPhysicalMoney;

    /// <summary>
    /// Gets or sets the current physical money.
    /// </summary>
    public float CurrentPhysicalMoney{
        get{
            return _currentPhysicalMoney;
        } 
        set{
            try{
                OnPhysicalMoneyUpdate.Invoke();
                if(value < 0f){
                    throw new Exception("Atempted to spend physical money and the value in the account got negative.");
                }
                else if(value > _currentPhysicalMoney){
                    OnPhysicalMoneyIncrease.Invoke();
                }
                else if (value < _currentPhysicalMoney){
                    OnPhysicalMoneyReduce.Invoke();
                }
                _currentPhysicalMoney = value;
            } catch (UnassignedReferenceException ex) {
                Debug.LogError("Error changing physical money value: " + ex.Message);
            }
        }
    }

    float _currentDebt;

    /// <summary>
    /// Gets or sets the current debt.
    /// </summary>
    public float CurrentDebt{
        get{
            return _currentDebt;
        } 
        set{
            try{
            OnDebtUpdate.Invoke();
            if(value < 0f){
                throw new Exception("Atempted to input a negative debt value.");
            }
            else if (value > CurrentMaxDebt){
                throw new Exception("Atempted to input a bigger than allowed debt value.");
            }
            else if(value > _currentDebt){
                OnDebtIncrease.Invoke();
            }
            else if (value < _currentDebt){
                OnDebtReduce.Invoke();
            }
            _currentDebt = value;
            } catch (UnassignedReferenceException ex) {
                Debug.LogError("Error changing debt value: " + ex.Message);
            }
        }
    }

    float _currentMaxDebt;
    
    /// <summary>
    /// Gets or sets the current maximum debt.
    /// </summary>
    public float CurrentMaxDebt{
        get{
            return _currentMaxDebt;
        } 
        set{
            try{
            OnMaxDebtUpdate.Invoke();
            if(value < 0f){
                throw new Exception("Atempted to input a negative max debt value.");
            }
            if(value > _currentMaxDebt){
                OnMaxDebtIncrease.Invoke();
            }
            else if (value < _currentMaxDebt){
                OnMaxDebtReduce.Invoke();
            }
            _currentMaxDebt = value;
            } catch (UnassignedReferenceException ex) {
                Debug.LogError("Error changing max debt value: " + ex.Message);
            }
        }
    }

    // Events for digital money
    public UnityEvent OnDigitalMoneyIncrease;
    public UnityEvent OnDigitalMoneyUpdate;
    public UnityEvent OnDigitalMoneyReduce;

    // Events for physical money
    public UnityEvent OnPhysicalMoneyIncrease;
    public UnityEvent OnPhysicalMoneyUpdate;
    public UnityEvent OnPhysicalMoneyReduce;

    // Events for debt
    public UnityEvent OnDebtIncrease;
    public UnityEvent OnDebtUpdate;
    public UnityEvent OnDebtReduce;

    // Events for max debt
    public UnityEvent OnMaxDebtIncrease;
    public UnityEvent OnMaxDebtUpdate;
    public UnityEvent OnMaxDebtReduce;

    /// <summary>
    /// Initializes the wallet with initial values.
    /// </summary>
    void Start(){
        CurrentDigitalMoney = _initialDigitalMoney;
        CurrentPhysicalMoney = _initialPhysicalMoney;
        CurrentDebt = _initialDebt;
        CurrentMaxDebt = _initialMaxDebt;
    }
}