/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UISystem
{
    public class Text : CustomUIComponent
    {
        public TextSO textData;
        public Style style;

        private TextMeshProUGUI textMeshProUGUI;

        protected override void Setup()
        {
            textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        }

        protected override void Configure()
        {
            ThemeSO theme = GetMainTheme();
            if (theme == null) throw new Exception("ERROR: ThemeManager missing or not found and no override theme added.");

            textMeshProUGUI.color = theme.GetTextColor(style);
            textMeshProUGUI.font = textData.font;
            textMeshProUGUI.fontSize = textData.size;
        }

        public void SetText(string text){
            textMeshProUGUI.text = text;
        }
    }
}