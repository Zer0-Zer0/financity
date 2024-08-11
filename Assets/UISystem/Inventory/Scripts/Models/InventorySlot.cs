using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        [SerializeField] private InventoryItem _currentItem;
        public InventoryItem CurrentItem
        {
            get { return _currentItem; }
            private set
            {
                if (_currentItem != null && value != _currentItem && _currentAmount != 0)
                {
                    throw new Exception("Attempted to change slot's item even though its amount is not null.");
                }
                _currentItem = value;
            }
        }

        [SerializeField] private int _currentAmount;
        public int CurrentAmount
        {
            get { return _currentAmount; }
            set
            {
                if (value < 0f)
                    throw new Exception("Attempted to change item amount in the inventory slot to negative.");
                if (!ItemIsNull)
                {
                    if (value > CurrentItem.MaxAmount)
                    {
                        throw new Exception("Attempted to change item amount in the inventory slot to above its limit.");
                    }
                    else if (value == 0)
                    {
                        _currentAmount = value;
                        CurrentItem = null;
                        return;
                    }
                }

                _currentAmount = value;
            }
        }

        public bool ItemIsNull { get { return CurrentItem == null; } }

        public InventorySlot(InventoryItem item = null, int amount = 0)
        {
            CurrentItem = item;
            CurrentAmount = amount;
        }

        public void SetItem(InventoryItem item, int amount)
        {
            if (ItemIsNull)
            {
                if (amount <= item.MaxAmount)
                {
                    CurrentAmount = amount;
                }
                else
                {
                    throw new Exception("Attempted to change item amount in the inventory to above its limit.");
                }
                CurrentItem = item;
            }
            else
            {
                throw new Exception("Attempted to change slot's item even though its amount is not null.");
            }
        }

        public override string ToString()
        {
            return $"Inventory Slot: {CurrentItem.ToString()} - Amount: {CurrentAmount}";
        }

    }
}