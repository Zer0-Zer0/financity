using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [Header("Inventory")]
        [SerializeField]
        private Inventory _initialInventory;

        [Header("Events")]
        [SerializeField]
        private GameEvent OnInventoryChanged;

        [SerializeField]
        private GameEvent OnInventoryValueChanged;

        [SerializeField]
        private GameEvent OnItemConsuption;

        private Inventory _inventory;

        private InventorySlot _selectedSlot;

        private void Awake() => _inventory = _initialInventory;

        private void Start() => RaiseEvents();

        private void OnEnable() => RaiseEvents();

        void Update() => CheckForInput();

        private void CheckForInput()
        {
            if (Input.GetKeyDown(KeyCode.E))
                ConsumeSelectedItem();
            if (Input.GetKeyDown(KeyCode.Q))
                RemoveSelectedItem(1);
        }

        private void RemoveSelectedItem(int amount)
        {
            bool isSlotNull = _selectedSlot == null;
            if (isSlotNull)
                return;

            bool isItemNull = _selectedSlot.CurrentItem == null;
            if (isItemNull)
                return;

            Inventory.SubtractItem(_inventory, _selectedSlot.CurrentItem, amount);
            OnItemConsuption.Raise(this, _selectedSlot);
            RaiseEvents();
        }

        private void ConsumeSelectedItem()
        {
            bool isSlotNull = _selectedSlot == null;
            if (isSlotNull)
                return;

            bool isItemNull = _selectedSlot.CurrentItem == null;
            if (isItemNull)
                return;

            _selectedSlot.CurrentItem.OnItemConsumeEvent.Raise(this, _selectedSlot.CurrentItem);
            RemoveSelectedItem(1);
        }

        private void RaiseEvents()
        {
            OnInventoryChanged.Raise(this, _inventory.GetInventorySlots());
            OnInventoryValueChanged.Raise(this, _inventory.GetInventoryValue());
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
            RaiseEvents();
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
            RaiseEvents();
        }

        public void OnEconomyTick(Component sender, object data)
        {
            OnInventoryValueChanged.Raise(this, _inventory.GetInventoryValue());
        }

        public void OnMouseHover(Component sender, object data)
        {
            if (data is InventorySlot slot)
                _selectedSlot = slot;
        }
    }
}
