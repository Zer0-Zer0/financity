/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace UISystem
{
    public class CustomButton : CustomUIComponent
    {
        public Style style;
        public UnityEvent onClickEvent;

        private Button button;
        private TextMeshProUGUI buttonText;

        protected override void Setup()
        {
            button = GetComponentInChildren<Button>();
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }

        protected override void Configure()
        {
            ThemeSO theme = GetMainTheme();
            if (theme == null) return;

            ColorBlock cb = button.colors;
            cb.normalColor = theme.GetBackgroundColor(style);
            button.colors = cb;

            buttonText.color = theme.GetTextColor(style);
        }

        private void OnEnable() {
            button.onClick.AddListener(OnClickMethod);
        }

        private void OnDisable(){
            button.onClick.RemoveListener(OnClickMethod);
        }

        public void OnClickMethod()
        {
            onClickEvent?.Invoke();
        }
    }
}