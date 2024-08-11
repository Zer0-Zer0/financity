using System;
using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UISystem;
using UnityEngine;

public class InventoryViewModel : MonoBehaviour
{
    [SerializeField]
    InventoryView _inventoryView;

    [SerializeField]
    List<InventorySlot> _inventorySlots;

    public void OnInventoryChanged(Component sender, object data)
    {
        if (data is List<InventorySlot> slots)
            _inventorySlots = slots;
        UpdateSlotsGraphics();
    }

    private void OnEnable() => UpdateSlotsGraphics();

    private void UpdateSlotsGraphics()
    {
        _inventoryView.UpdateSlotsGraphics(_inventorySlots);
    }
}
