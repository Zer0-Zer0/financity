using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

class WalletManager : MonoBehaviour
{
    [SerializeField] float _initialDigitalMoney = 800f;
    [SerializeField] float _initialPhysicalMoney = 0f;
    [SerializeField] float _initialDebt = 800f;
    [SerializeField] float _initialMaxDebt = 800f;

    float _currentDigitalMoney;

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

    public UnityEvent OnDigitalMoneyIncrease;
    public UnityEvent OnDigitalMoneyUpdate;
    public UnityEvent OnDigitalMoneyReduce;

    public UnityEvent OnPhysicalMoneyIncrease;
    public UnityEvent OnPhysicalMoneyUpdate;
    public UnityEvent OnPhysicalMoneyReduce;

    public UnityEvent OnDebtIncrease;
    public UnityEvent OnDebtUpdate;
    public UnityEvent OnDebtReduce;

    public UnityEvent OnMaxDebtIncrease;
    public UnityEvent OnMaxDebtUpdate;
    public UnityEvent OnMaxDebtReduce;

    void Start(){
        CurrentDigitalMoney = _initialDigitalMoney;
        CurrentPhysicalMoney = _initialPhysicalMoney;
        CurrentDebt = _initialDebt;
        CurrentMaxDebt = _initialMaxDebt;
    }
}