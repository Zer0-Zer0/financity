using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class InventoryView : CustomUIComponent
    {
        [SerializeField]
        private Style _style;

        [SerializeField]
        private Image _backgroundImage;

        [SerializeField]
        private GameObject _InventorySlotsParent;
        private List<InventorySlotView> _InventorySlotViews;

        [SerializeField]
        private InventorySlotView _InventorySlotViewPrefab; // Prefab for the inventory slot view

        private int _slotCount
        {
            get { return _InventorySlotViews.Count; }
        }

        protected override void Setup()
        {
            _InventorySlotViews = new List<InventorySlotView>(
                _InventorySlotsParent.GetComponentsInChildren<InventorySlotView>()
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
                InventorySlotView newSlotView = Instantiate(
                    _InventorySlotViewPrefab,
                    _InventorySlotsParent.transform
                );
                _InventorySlotViews.Add(newSlotView);
            }
        }

        private void RemoveSlots(int slotsToRemove)
        {
            for (int i = 0; i < slotsToRemove; i++)
            {
                InventorySlotView slotToRemove = _InventorySlotViews[_InventorySlotViews.Count - 1];
                _InventorySlotViews.RemoveAt(_InventorySlotViews.Count - 1);
                Destroy(slotToRemove.gameObject);
            }
        }

        private void UpdateSlotGraphics(List<InventorySlot> slots)
        {
            for (int index = 0; index < slots.Count; index++)
            {
                _InventorySlotViews[index].UpdateSlotGraphics(slots[index]);
            }
        }
    }
}
