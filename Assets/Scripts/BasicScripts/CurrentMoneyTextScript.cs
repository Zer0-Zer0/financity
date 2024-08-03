using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_Text))]
public class CurrentMoneyTextScript : MonoBehaviour
{
    private TMP_Text _text;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
        //DataManager.playerData.CurrentBalanceChanged.AddListener(OnCurrentBalanceChanged); For some reason aint working
    }

    void Update(){
        OnCurrentBalanceChanged();
    }

    void OnCurrentBalanceChanged()
    {
        _text.text = String.Format("{0:C2} BRL", DataManager.playerData.GetCurrentBalance());
    }
}
