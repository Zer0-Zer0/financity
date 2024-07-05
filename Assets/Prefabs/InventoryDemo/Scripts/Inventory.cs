using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] slots;

    public int AddItem(InventoryItem item, int amount)
    {
        int remainingItems = amount;

        foreach (InventorySlot slot in slots)
        {
            int spaceLeftInSlot = item.MaxAmount - slot.CurrentAmount;
            if (slot.CurrentItem == item)
            {
                if (spaceLeftInSlot >= remainingItems)
                {
                    slot.CurrentAmount += remainingItems;
                    remainingItems = 0;
                    return remainingItems;
                }
                else
                {
                    slot.CurrentAmount = item.MaxAmount;
                    remainingItems -= spaceLeftInSlot;
                }
            }
            else if (IsSlotEmpty(slot))
            {
                if (spaceLeftInSlot >= remainingItems)
                {
                    slot.AddItem(item, remainingItems);
                    remainingItems = 0;
                    return remainingItems;
                }
                else
                {
                    slot.AddItem(item, item.MaxAmount);
                    remainingItems -= spaceLeftInSlot;
                }
            }
        }

        return remainingItems;
    }

    public bool IsSlotEmpty(InventorySlot slot)
    {
        return slot.CurrentItem == null;
    }

    public int RemoveItem(InventoryItem item, int amount)
    {
        int remainingItems = amount;

        foreach (InventorySlot slot in slots)
        {
            if (slot.CurrentItem == item)
            {
                if (slot.CurrentAmount >= remainingItems)
                {
                    slot.CurrentAmount -= remainingItems;
                    if (slot.CurrentAmount <= 0)
                    {
                        slot.CurrentAmount = 0;
                    }
                    return 0; // No missing items
                }
                else
                {
                    remainingItems -= slot.CurrentAmount;
                    slot.CurrentAmount = 0;
                }
            }
        }

        return remainingItems; // Return the remaining items that could not be removed
    }


    public override string ToString()
    {
        string result = "Inventory Slots:\n";
        foreach (InventorySlot slot in slots)
        {
            if (slot.CurrentItem != null)
            {
                result += slot.CurrentItem.ToString() + ", Current Amount: " + slot.CurrentAmount + "\n";
            }
        }
        return result;
    }
}
