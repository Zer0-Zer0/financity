using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class InventorySlotView : CustomUIComponent
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
        private TextMeshProUGUI _itemCounter;

        protected override void Setup() { }

        protected override void Configure()
        {
            ThemeSO theme = GetMainTheme();
            if (theme == null)
                return;

            _slotBackground.color = theme.GetBackgroundColor(_style);
            _itemCounter.color = theme.GetTextColor(_style);
            _itemCounter.font = _textData.font;
            _itemCounter.fontSize = _textData.size;
        }

        public void UpdateSlotGraphics(InventorySlot slot)
        {
            if (slot == null || slot.CurrentItem == null)
                Reset();
            else
                SetGraphics(slot);
        }

        private void SetGraphics(InventorySlot slot)
        {
            if (slot.CurrentAmount <= 0)
                throw new ArgumentOutOfRangeException(
                    "CurrentAmount needs to be bigger than zero."
                );

            if (slot.CurrentItem == null)
            {
                Reset();
                return;
            }

            _slotIcon.sprite = slot.CurrentItem.Icon;
            _slotIcon.color = Color.white;
            _itemCounter.SetText(slot.CurrentAmount.ToString());
        }

        private void Reset()
        {
            _slotIcon.sprite = null;
            _itemCounter.SetText(String.Empty);
            _slotIcon.color = new Color(0, 0, 0, 0);
        }
    }
}
