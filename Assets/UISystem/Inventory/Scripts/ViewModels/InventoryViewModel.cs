using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;
using Inventory;
public class InventoryViewModel : MonoBehaviour
{
    [SerializeField] private InventoryView _inventoryView;

    public void OnInventoryChanged(Component sender, object data)
    {
        if (data is List<InventorySlot> slots)
            _inventoryView.UpdateSlotsGraphics(slots);
    }
}
