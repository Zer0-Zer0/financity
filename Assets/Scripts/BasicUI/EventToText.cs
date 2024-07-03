using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EventToText : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] string _textFormmating = "";
    public void FloatToText(float value){
        _text.text = String.Format(_textFormmating, value);
    }

    public void StringToText(string value){
        _text.text = value;
    }

    public void TimeToText(int value){
        _text.text = TimeManager.FormatTime(value);
    }

    public void ClockToText(ClockString value){
        _text.text = value.time;
    }
}
