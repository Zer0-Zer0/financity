using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;

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
        private List<InventorySlotViewModel> _InventorySlotViewModels;

        [SerializeField]
        private InventorySlotViewModel _InventorySlotViewPrefab;
        private int _slotCount
        {
            get { return _InventorySlotViewModels.Count; }
        }

        protected override void Setup()
        {
            _InventorySlotViewModels = new List<InventorySlotViewModel>(
                _InventorySlotsParent.GetComponentsInChildren<InventorySlotViewModel>()
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
                InventorySlotViewModel newSlotView = Instantiate(
                    _InventorySlotViewPrefab,
                    _InventorySlotsParent.transform
                );
                _InventorySlotViewModels.Add(newSlotView);
            }
        }

        private void RemoveSlots(int slotsToRemove)
        {
            for (int i = 0; i < slotsToRemove; i++)
            {
                InventorySlotViewModel slotToRemove = _InventorySlotViewModels[_InventorySlotViewModels.Count - 1];
                _InventorySlotViewModels.RemoveAt(_InventorySlotViewModels.Count - 1);
                Destroy(slotToRemove.gameObject);
            }
        }

        private void UpdateSlotGraphics(List<InventorySlot> slots)
        {
            for (int index = 0; index < slots.Count; index++)
            {
                _InventorySlotViewModels[index].UpdateSlotGraphics(slots[index]);
            }
        }
    }
}
