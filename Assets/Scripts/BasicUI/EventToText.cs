using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TMP_Text))]
public class EventToText : MonoBehaviour
{
    [SerializeField] string _textFormmating = "";
    TMP_Text text;
    void Awake(){
        text = GetComponent<TMP_Text>();
    }

    public void FloatToText(float value){
        text.text = String.Format(_textFormmating, value);
    }

}
