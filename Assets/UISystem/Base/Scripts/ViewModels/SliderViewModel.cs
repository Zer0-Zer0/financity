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
    public class SliderViewModel : MonoBehaviour
    {
        [SerializeField] private CustomSlider _slider;

        public void OnValueChanged(Component sender, object data)
        {
            if (data is float floatValue)
                _slider.SetValue(floatValue);
            else if(data is int intValue)
                _slider.SetValue(intValue);
        }
    }
}