using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;

namespace UISystem
{
    public class ItemDescriptionView : CustomUIComponent
    {
        [SerializeField]
        Image _itemIcon;

        [SerializeField]
        Text _itemName;

        [SerializeField]
        Text _itemDescription;

        [SerializeField]
        Text _itemCurrentAmount;

        [SerializeField]
        Text _itemTotalAmount;

        [SerializeField]
        Text _itemValue;

        [SerializeField]
        GameObject _hideWhenNoItem;

        protected override void Setup() { }

        protected override void Configure() { }

        public void UpdateItemDescription(InventorySlot slot)
        {
            bool isSlotNull = slot == null;
            bool isItemNull = slot.CurrentItem == null;
            if (isSlotNull || isItemNull)
                Reset();
            else
                SetItemDescription(slot);
        }

        private void SetItemDescription(InventorySlot slot)
        {
            _itemIcon.sprite = slot.CurrentItem.Icon;
            _itemName.SetText(slot.CurrentItem.Name);
            _itemDescription.SetText(slot.CurrentItem.Description);
            _itemCurrentAmount.SetText(slot.CurrentAmount.ToString());
            _itemTotalAmount.SetText(slot.CurrentItem.MaxAmount.ToString());
            UpdateItemValue(slot);
            _hideWhenNoItem.SetActive(true);
        }

        public void UpdateItemValue(InventorySlot slot)
        {
            _itemValue.SetText(String.Format("{0:N2}BRL", slot.CurrentItem.GetCurrentValue()));
        }

        public void Reset()
        {
            _hideWhenNoItem.SetActive(false);
        }
    }
}
