using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace UISystem
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private Inventory _initialInventory;

        [Header("Event")]
        [SerializeField]
        private GameEvent OnInventoryChanged;

        private Inventory _inventory;

        private void Awake()
        {
            _inventory = _initialInventory;
        }

        private void Start()
        {
            OnInventoryChanged.Raise(this, _inventory.GetInventorySlots());
        }

        public void OnInventoryItemSubtracted(Component sender, object data)
        {
            if (data is Inventory items)
                Inventory.SubtractItem(_inventory, items);
            else if (data is InventorySlot slot)
                Inventory.SubtractItem(_inventory, slot);
            else
                throw new InvalidDataException(
                    $"ERROR: Not possible to subtract from inventory data of type {data.GetType()} sent from {sender}"
                );
            OnInventoryChanged.Raise(this, _inventory.GetInventorySlots());
        }

        public void OnInventoryItemAdded(Component sender, object data)
        {
            if (data is Inventory items)
                Inventory.AddItem(_inventory, items);
            else if (data is InventorySlot slot)
                Inventory.AddItem(_inventory, slot);
            else
                throw new InvalidDataException(
                    $"ERROR: Not possible to add to inventory data of type {data.GetType()} sent from {sender}"
                );
            OnInventoryChanged.Raise(this, _inventory.GetInventorySlots());
        }
    }
}
