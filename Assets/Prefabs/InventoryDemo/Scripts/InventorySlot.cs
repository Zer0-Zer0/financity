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
        set
        {
            try
            {
                if (_currentItem != null)
                {
                    throw new Exception("Changed item in the inventory even though its amount is not null.");
                }
                _currentItem = value;
                ItemChanged?.Invoke(this);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Potential problematic action during the slot's item change: " + ex.Message);
            }
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
            try
            {
                if (value < 0f)
                {
                    throw new Exception("Attempted to change item amount in the inventory to negative.");
                }
                else if (value > CurrentItem.MaxAmount)
                {
                    throw new Exception("Attempted to change item amount in the inventory to above its limit.");
                }
                _currentAmount = value;

                if (value == 0)
                {
                    SlotCleared?.Invoke(this);
                }
                else
                {
                    AmountChanged?.Invoke(this);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error changing the slot's item amount: " + ex.Message);
            }
        }
    }
}
