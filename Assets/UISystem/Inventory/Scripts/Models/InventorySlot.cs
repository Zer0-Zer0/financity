using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySlot
{
    public UnityEvent<InventorySlot> ItemChanged;
    public UnityEvent<InventorySlot> AmountChanged;
    public UnityEvent<InventorySlot> SlotCleared;

    [SerializeField] private InventoryItem _currentItem;
    public InventoryItem CurrentItem
    {
        get
        {
            return _currentItem;
        }
        private set
        {
            if (_currentItem != null && value != _currentItem && _currentAmount != 0)
            {
                throw new Exception("Attempted to change slot's item even though its amount is not null.");
            }
            _currentItem = value;
            ItemChanged?.Invoke(this);
        }
    }

    [SerializeField] private int _currentAmount;
    public int CurrentAmount
    {
        get
        {
            return _currentAmount;
        }
        set
        {
            if (value < 0f)
            {
                throw new Exception("Attempted to change item amount in the inventory to negative.");
            }
            if (!ItemIsNull)
            {
                if (value > CurrentItem.MaxAmount)
                {
                    throw new Exception("Attempted to change item amount in the inventory to above its limit.");
                }
                else if (value == 0)
                {
                    _currentAmount = value;
                    CurrentItem = null;
                    SlotCleared?.Invoke(this);
                    return;
                }
            }

            _currentAmount = value;
            AmountChanged?.Invoke(this);
        }
    }

    public bool ItemIsNull
    {
        get
        {
            return CurrentItem == null;
        }
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
