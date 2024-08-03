// Fonte de inspiração: https://youtu.be/oOQvhIg0ntg
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
            textMeshProUGUI.color = textData.theme.GetTextColor(style);
            textMeshProUGUI.font = textData.font;
            textMeshProUGUI.fontSize = textData.size;
        }
    }
}