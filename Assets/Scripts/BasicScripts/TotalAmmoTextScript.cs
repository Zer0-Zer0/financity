using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_Text))]
public class TotalAmmoTextScript : MonoBehaviour
{
    private TMP_Text _text;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
        //DataManager.playerData.TotalAmmoChanged.AddListener(OnTotalAmmoChanged); For some reason aint working
    }

    void Update()
    {
        OnTotalAmmoChanged();
    }

    void OnTotalAmmoChanged()
    {
        _text.text = DataManager.playerData.GetTotalAmmo().ToString();
    }
}
