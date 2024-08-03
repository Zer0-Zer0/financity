using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UISystem
{
    public class InventoryView : CustomUIComponent
    {
        [SerializeField] private Style _style;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private GameObject _InventorySlotsParent;
        private List<InventorySlotView> _InventorySlotViews;

        private float _slotCount{ get{ return _InventorySlotViews.Count;} }

        protected override void Setup()
        {
            _InventorySlotViews = new List<InventorySlotView>(_InventorySlotsParent.GetComponentsInChildren<InventorySlotView>());
        }

        protected override void Configure()
        {
            ThemeSO theme = GetMainTheme();
            if (theme == null) return;

            _backgroundImage.color = theme.GetBackgroundColor(_style);
        }

        public void UpdateSlotsGraphics(List<InventorySlot> slots){
            for(int index = 0; index < _slotCount; index++){
                _InventorySlotViews[index].UpdateSlotGraphics(slots[index]);
            }
        }
    }
}