using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        protected override void Setup() { }

        protected override void Configure() { }

        public void SetItemDescription(InventorySlot slot)
        {
            _itemIcon.sprite = slot.CurrentItem.Icon;
            _itemName.SetText(slot.CurrentItem.Name);
            _itemDescription.SetText(slot.CurrentItem.Description);
            _itemCurrentAmount.SetText(slot.CurrentAmount.ToString());
            _itemTotalAmount.SetText(slot.CurrentItem.MaxAmount.ToString());
            UpdateItemValue(slot);
        }

        public void UpdateItemValue(InventorySlot slot)
        {
            _itemValue.SetText(slot.CurrentItem.GetCurrentValue().ToString());
        }
    }
}
