using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public class InventoryViewModel : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private GameEvent _inventoryChanged;

        public void OnInventoryChanged(Component sender, object data)
        {
            if (data is List<InventorySlot> slots)
                _inventoryView.UpdateSlotsGraphics(slots);
        }
    }
}