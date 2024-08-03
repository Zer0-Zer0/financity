using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UISystem
{
    public class CustomButton : CustomUIComponent
    {
        public ThemeSO theme;
        public Style style;
        public GameEvent onClickEvent;

        private Button button;
        private TextMeshProUGUI buttonText;

        protected override void Setup(){
            button = GetComponentInChildren<Button>();
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }

        protected override void Configure(){
            ColorBlock cb = button.colors;
            cb.normalColor = theme.GetBackgroundColor(style);
            button.colors = cb;

            buttonText.color = theme.GetTextColor(style);            
        }

        public void OnClick(){
            onClickEvent.Raise(this, null);
        }
    }
}