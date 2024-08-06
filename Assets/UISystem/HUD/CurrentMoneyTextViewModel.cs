using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UISystem
{
    //TODO: Modernize this to use game events
    public class CurrentMoneyTextViewModel : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        void Start()
        {
            DataManager.playerData.CurrentBalanceChanged.AddListener(OnCurrentBalanceChanged);
            OnCurrentBalanceChanged();
        }

        void OnCurrentBalanceChanged()
        {
            string _formatedText = String.Format(
                "{0:N2}BRL",
                DataManager.playerData.GetCurrentBalance()
            );
            _text.SetText(_formatedText);
        }
    }
}
