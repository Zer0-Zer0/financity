/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UISystem
{
    public class InventorySlotViewer : CustomUIComponent
    {
        [SerializeField] private InventorySlot _inventorySlot;
        [SerializeField] private Image _slotIcon;
        [SerializeField] private Image _slotBackground;
        [SerializeField] private TextMeshProUGUI _itemCounter;
        [SerializeField] private TextSO textData;
        [SerializeField] private Style style;
        protected override void Setup()
        {

        }

        protected override void Configure()
        {
            ThemeSO theme = GetMainTheme();
            if (theme == null) return;

            _slotBackground.color = theme.GetBackgroundColor(style);

            _itemCounter.color = theme.GetTextColor(style);
            _itemCounter.font = textData.font;
            _itemCounter.fontSize = textData.size;
        }

        private void OnEnable()
        {
            _inventorySlot.ItemChanged.AddListener(UpdateSlotGraphics);
        }

        private void OnDisable(){
            _inventorySlot.ItemChanged.RemoveListener(UpdateSlotGraphics);
        }

        public void UpdateSlotGraphics(InventorySlot slot)
        {
            _slotIcon.sprite = _inventorySlot.CurrentItem.Icon;
            _itemCounter.SetText(_inventorySlot.CurrentAmount.ToString());
        }

        public void SetCurrentInventorySlot(InventorySlot item)
        {
            _inventorySlot = item;
        }

        public InventorySlot GetCurrentInventorySlot()
        {
            return _inventorySlot;
        }
    }
}