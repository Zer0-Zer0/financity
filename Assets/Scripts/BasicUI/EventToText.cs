using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TMP_Text))]
public class EventToText : MonoBehaviour
{
    private TMP_Text _text;
    [SerializeField] string _textFormmating = "";

    void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void FloatToText(EventObject value)
    {
        _text.text = String.Format(_textFormmating, value.floatingPoint);
    }

    public void StringToText(EventObject value)
    {
        _text.text = value.text;
    }
}
