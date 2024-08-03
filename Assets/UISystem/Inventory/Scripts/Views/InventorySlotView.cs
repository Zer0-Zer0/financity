using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UISystem
{
    public class InventorySlotView : CustomUIComponent
    {
        [SerializeField] private TextSO _textData;
        [SerializeField] private Style _style;
        
        [Header("Structure")]
        [SerializeField] private Image _slotIcon;
        [SerializeField] private Image _slotBackground;
        [SerializeField] private TextMeshProUGUI _itemCounter;
        
        protected override void Setup() { }

        protected override void Configure()
        {
            ThemeSO theme = GetMainTheme();
            if (theme == null) return;

            _slotBackground.color = theme.GetBackgroundColor(_style);

            _itemCounter.color = theme.GetTextColor(_style);
            _itemCounter.font = _textData.font;
            _itemCounter.fontSize = _textData.size;
        }

        public void UpdateSlotGraphics(InventorySlot slot)
        {
            _slotIcon.sprite = slot.CurrentItem.Icon;
            if (slot.CurrentAmount == 0)
                _itemCounter.SetText(String.Empty);
            else
                _itemCounter.SetText(slot.CurrentAmount.ToString());
        }
    }
}