using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

class WalletManager : MonoBehaviour
{
    [SerializeField] float _initialDigitalMoney = 800f;
    [SerializeField] float _initialPhysicalMoney = 0f;

    float _currentDigitalMoney;

    public float CurrentDigitalMoney{
        get;
        set{
            OnDigitalMoneyUpdate.Invoke();

            if(value > _currentDigitalMoney){
                OnDigitalMoneyIncrease.Invoke();
            }
            else if (value < _currentDigitalMoney)
            {
                OnDigitalMoneyReduce.Invoke();
            }
            _currentDigitalMoney = value;
        }
    }

    float _currentPhysicalMoney;

    public float CurrentPhysicalMoney{
        get; 
        set{
            OnPhysicalMoneyUpdate.Invoke();
            if(value < 0f){
                Debug.LogError("ERRO! Tentativa de comprar algo com dinheiro físico e valor ficou negativado, alguém esqueceu um if em algum lugar <-<");
            }
            if(value > _currentPhysicalMoney){
                OnPhysicalMoneyIncrease.Invoke();
            }
            else if (value < _currentPhysicalMoney)
            {
                OnPhysicalMoneyReduce.Invoke();
            }
            _currentPhysicalMoney = value;
        }
    }

    public UnityEvent OnDigitalMoneyIncrease;
    public UnityEvent OnDigitalMoneyUpdate;
    public UnityEvent OnDigitalMoneyReduce;

    public UnityEvent OnPhysicalMoneyIncrease;
    public UnityEvent OnPhysicalMoneyUpdate;
    public UnityEvent OnPhysicalMoneyReduce;

    void Start(){
        CurrentDigitalMoney = _initialDigitalMoney;
        CurrentPhysicalMoney = _initialPhysicalMoney;
    }
}