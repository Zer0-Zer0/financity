using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;

namespace UISystem
{
    public class StoreSlotView : CustomUIComponent
    {
        [SerializeField]
        private TextSO _textData;

        [SerializeField]
        private Style _style;

        [Header("Structure")]
        [SerializeField]
        private Image _slotIcon;

        [SerializeField]
        private Image _slotBackground;

        [SerializeField]
        private Text _itemValue;

        protected override void Setup() { }

        protected override void Configure()
        {
            ThemeSO theme = GetMainTheme();
            if (theme == null)
                return;

            _slotBackground.color = theme.GetBackgroundColor(_style);
        }

        public void UpdateSlotGraphics(InventoryItem item)
        {
            if (item == null)
                Reset();
            else
                SetGraphics(item);
        }

        private void SetGraphics(InventoryItem item)
        {
            if (item == null)
            {
                Reset();
                return;
            }

            _slotIcon.sprite = item.Icon;
            _slotIcon.color = Color.white;
            float itemValue = item.GetCurrentValue();
            string formattedText = String.Format("{0:N2}BRL", itemValue);
            _itemValue.SetText(formattedText);
        }

        private void Reset()
        {
            _slotIcon.sprite = null;
            _itemValue.SetText(String.Empty);
            _slotIcon.color = new Color(0, 0, 0, 0);
        }
    }
}
