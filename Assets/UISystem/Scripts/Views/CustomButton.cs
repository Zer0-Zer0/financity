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
        public UnityEvent onClick;

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
            if (theme == null) throw new Exception("ERROR: ThemeManager missing or not found and no override theme added.");

            ColorBlock cb = button.colors;
            cb.normalColor = theme.GetBackgroundColor(style);
            button.colors = cb;

            buttonText.color = theme.GetTextColor(style);
        }

        public void OnClick()
        {
            onClick?.Invoke();
        }
    }
}