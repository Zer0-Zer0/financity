using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_Text))]
public class CurrentAmmoTextScript : MonoBehaviour
{
    private TMP_Text _text;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
        //DataManager.playerData.CurrentAmmoChanged.AddListener(OnCurrentAmmoChanged); For some reason aint working
    }

    void Update()
    {
        OnCurrentAmmoChanged();
    }

    void OnCurrentAmmoChanged()
    {
        _text.text = DataManager.playerData.GetCurrentAmmo().ToString();
    }
}
