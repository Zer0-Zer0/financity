using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;

namespace UISystem
{
    public class StoreView : CustomUIComponent
    {
        [SerializeField]
        private Style _style;

        [SerializeField]
        private Image _backgroundImage;

        [SerializeField]
        private GameObject _StoreSlotsParent;
        private List<StoreSlotView> _StoreSlotViews;

        [SerializeField]
        private StoreSlotView _StoreSlotViewPrefab;
        private int _slotCount
        {
            get { return _StoreSlotViews.Count; }
        }

        protected override void Setup()
        {
            _StoreSlotViews = new List<StoreSlotView>(
                _StoreSlotViewPrefab.GetComponentsInChildren<StoreSlotView>()
            );
        }

        protected override void Configure()
        {
            ThemeSO theme = GetMainTheme();
            if (theme == null)
                return;

            _backgroundImage.color = theme.GetBackgroundColor(_style);
        }

        public void UpdateSlotsGraphics(List<InventorySlot> slots)
        {
            AdjustSlotCount(slots.Count);
            UpdateSlotGraphics(slots);
        }

        private void AdjustSlotCount(int targetCount)
        {
            if (targetCount > _slotCount)
                AddSlots(targetCount - _slotCount);
            else if (targetCount < _slotCount)
                RemoveSlots(_slotCount - targetCount);
        }

        private void AddSlots(int slotsToAdd)
        {
            for (int i = 0; i < slotsToAdd; i++)
            {
                StoreSlotView newSlotView = Instantiate(_StoreSlotViewPrefab);
                _StoreSlotViews.Add(newSlotView);
            }
        }

        private void RemoveSlots(int slotsToRemove)
        {
            for (int i = 0; i < slotsToRemove; i++)
            {
                StoreSlotView slotToRemove = _StoreSlotViews[_StoreSlotViews.Count - 1];
                _StoreSlotViews.RemoveAt(_StoreSlotViews.Count - 1);
                Destroy(slotToRemove.gameObject);
            }
        }

        private void UpdateSlotGraphics(List<InventorySlot> slots)
        {
            for (int index = 0; index < slots.Count; index++)
            {
                _StoreSlotViews[index].UpdateSlotGraphics(slots[index].CurrentItem);
            }
        }
    }
}
