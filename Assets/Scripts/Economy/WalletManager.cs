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
            OnDigitalMoneyUpdate.Invoke();
            if(value < 0f){
                Debug.LogError("ERRO! Tentativa de comprar algo com dinheiro digital e valor ficou negativado, alguém esqueceu de um if em algum lugar <-<");
            }
            else if(value > _currentDigitalMoney){
                OnDigitalMoneyIncrease.Invoke();
            }
            else if (value < _currentDigitalMoney){
                OnDigitalMoneyReduce.Invoke();
            }
            _currentDigitalMoney = value;
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
            OnPhysicalMoneyUpdate.Invoke();
            if(value < 0f){
                Debug.LogError("ERRO! Tentativa de comprar algo com dinheiro físico e valor ficou negativado, alguém esqueceu de um if em algum lugar <-<");
            }
            else if(value > _currentPhysicalMoney){
                OnPhysicalMoneyIncrease.Invoke();
            }
            else if (value < _currentPhysicalMoney){
                OnPhysicalMoneyReduce.Invoke();
            }
            _currentPhysicalMoney = value;
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
            OnDebtUpdate.Invoke();
            if(value < 0f){
                Debug.LogError("ERRO! Tentativa de ter um débito negativo, alguém esqueceu de um if em algum lugar <-<");
            }
            else if (value > CurrentMaxDebt){
                Debug.LogError("ERRO! Tentativa de ter um débito maior que o permitido, alguém esqueceu de um if em algum lugar <-<");
            }
            if(value > _currentDebt){
                OnDebtIncrease.Invoke();
            }
            else if (value < _currentDebt){
                OnDebtReduce.Invoke();
            }
            _currentDebt = value;
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
            OnMaxDebtUpdate.Invoke();
            if(value < 0f){
                Debug.LogError("ERRO! Tentativa de ter um débito máximo negativo, alguém esqueceu de um if em algum lugar <-<");
            }
            if(value > _currentMaxDebt){
                OnMaxDebtIncrease.Invoke();
            }
            else if (value < _currentMaxDebt){
                OnMaxDebtReduce.Invoke();
            }
            _currentMaxDebt = value;
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