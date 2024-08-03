/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class CustomSlider : CustomUIComponent
    {
        public SliderSO sliderData;

        public Image BackgroundImage;
        public Image FillImage;

        private Slider slider;
        protected override void Setup()
        {
            slider = GetComponentInChildren<Slider>();
        }

        protected override void Configure()
        {
            slider.interactable = sliderData.interactable;
            slider.minValue = sliderData.min;
            slider.maxValue = sliderData.max;
            BackgroundImage.color = sliderData.backgroundColor;
            FillImage.color = sliderData.fillColor;
        }

        public void SetValue(float value)
        {
            slider.value = value;
        }
    }
}