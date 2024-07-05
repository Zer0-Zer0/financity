using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] slots;

    public void AddItem(InventoryItem item, int amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.CurrentItem == item)
            {
                slot.CurrentAmount += amount;
                return;
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].CurrentItem == null)
            {
                slots[i].CurrentItem = item;
                slots[i].CurrentAmount = amount;
                return;
            }
        }
    }

    public void RemoveItem(InventoryItem item, int amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.CurrentItem == item)
            {
                slot.CurrentAmount -= amount;
                if (slot.CurrentAmount <= 0)
                {
                    slot.CurrentItem = null;
                    slot.CurrentAmount = 0;
                }
                return;
            }
        }
    }

    public override string ToString()
    {
        string result = "Inventory Slots:\n";
        foreach (InventorySlot slot in slots)
        {
            if (slot.CurrentItem != null)
            {
                result += slot.CurrentItem.name + " - " + slot.CurrentAmount + "\n";
            }
        }
        return result;
    }
}
