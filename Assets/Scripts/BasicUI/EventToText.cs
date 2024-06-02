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

}
