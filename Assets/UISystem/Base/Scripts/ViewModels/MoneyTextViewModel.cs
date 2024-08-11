/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public class MoneyTextViewModel : TextViewModel
    {
        public override void OnValueChanged(Component sender, object data)
        {
            if (data is float number)
            {
                string dataString = String.Format("{0:N2}BRL", number);
                _textView.SetText(dataString);
            }
        }
    }
}
