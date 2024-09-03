using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using Economy;

namespace InventorySystem
{
    public class InventoryController : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField]
        private GameEvent OnInventoryChanged;

        [SerializeField]
        private GameEvent OnInventoryValueChanged;

        [SerializeField]
        private GameEvent OnItemConsuption;

        private Inventory _currentInventory;
        public Inventory CurrentInventory
        {
            get
            {
                Inventory savedInventory = DataManager.playerData.GetCurrentInventory();
                if (_currentInventory == null)
                    _currentInventory = DataManager.playerData.GetCurrentInventory();
                return _currentInventory;
            }
            set
            {
                _currentInventory = value;
                SaveInventory();
            }
        }

        private InventorySlot _selectedSlot;

        private void Start() => RaiseEvents();

        private void OnEnable() => RaiseEvents();

        void Update() => CheckForInput();

        private void SaveInventory() =>
            DataManager.playerData.SetCurrentInventory(CurrentInventory);

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

            Inventory.SubtractItem(CurrentInventory, _selectedSlot.CurrentItem, amount);
            OnItemConsuption.Raise(this, _selectedSlot);
            SaveInventory();
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
            OnInventoryChanged.Raise(this, CurrentInventory.GetInventorySlots());
            OnInventoryValueChanged.Raise(this, CurrentInventory.GetInventoryValue());
        }

        public void OnInventoryItemSubtracted(Component sender, object data)
        {
            if (data is Inventory items)
                Inventory.SubtractItem(CurrentInventory, items);
            else if (data is InventorySlot slot)
                Inventory.SubtractItem(CurrentInventory, slot);
            else
                throw new InvalidDataException(
                    $"ERROR: Not possible to subtract from inventory data of type {data.GetType()} sent from {sender}"
                );
            RaiseEvents();
        }

        public void OnInventoryItemAdded(Component sender, object data)
        {
            if (data is Inventory items)
                Inventory.AddItem(CurrentInventory, items);
            else if (data is InventorySlot slot)
                Inventory.AddItem(CurrentInventory, slot);
            else
                throw new InvalidDataException(
                    $"ERROR: Not possible to add to inventory data of type {data.GetType()} sent from {sender}"
                );
            SaveInventory();
            RaiseEvents();
        }

        public void OnBuyItem(Component sender, object data)
        {
            if (data is InventorySlot slot)
            {
                float _itemValue = slot.CurrentItem.GetCurrentValue();
                if (DataManager.playerData.GetCurrentBalance() >= _itemValue)
                {
                    Inventory.AddItem(CurrentInventory, slot);

                    Transaction transaction = new Transaction(slot.CurrentItem.GetCurrentValue(), null, DataManager.playerData.GetCurrentWallet(), slot.CurrentItem.Name);
                    DataManager.playerData.AddTransaction(transaction);
                }
            }
            else
                throw new InvalidDataException(
                    $"ERROR: Not possible to add to inventory data of type {data.GetType()} sent from {sender}"
                );
            SaveInventory();
            RaiseEvents();
        }

        public void OnEconomyTick(Component sender, object data)
        {
            OnInventoryValueChanged.Raise(this, CurrentInventory.GetInventoryValue());
        }

        public void OnMouseHover(Component sender, object data)
        {
            if (data is InventorySlot slot)
                _selectedSlot = slot;
        }
    }
}
