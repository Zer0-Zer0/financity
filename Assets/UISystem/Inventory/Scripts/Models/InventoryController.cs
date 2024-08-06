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
        [Header("UI structure")]
        [SerializeField]
        private Text _currentTotalInventoryValue;

        [Header("Inventory")]
        [SerializeField]
        private Inventory _initialInventory;

        [Header("Events")]
        [SerializeField]
        private GameEvent OnInventoryChanged;

        [SerializeField]
        private GameEvent OnInventoryValueChanged;

        private Inventory _inventory;

        private void Awake()
        {
            _inventory = _initialInventory;
        }

        private void Start()
        {
            RaiseEvents();
            UpdateText();
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
            UpdateText();
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
            UpdateText();
        }

        private void UpdateText()
        {
            string formattedText = String.Format("{0:C2}BRL", _inventory.GetInventoryValue());
            _currentTotalInventoryValue.SetText(formattedText);
        }
    }
}
