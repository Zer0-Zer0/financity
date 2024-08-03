/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public class TextViewModel : MonoBehaviour
    {
        [SerializeField] private Text _textView;

        public void OnValueChanged(Component sender, object data){
            if (data is not null){
                    string dataString = data.ToString();
                    _textView.SetText(dataString);
            }
        }
    }
}