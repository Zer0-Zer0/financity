using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UISystem
{
    public class Inventory :
    {
        public List<InventorySlot> slots = new List<InventorySlot>();

        public int CurrentSlotCount
        {
            get
            {
                return slots.Count;
            }
        }

        public void ExpandSlots(int additionalSlots)
        {
            for (int i = 0; i < additionalSlots; i++)
            {
                slots.Add(new InventorySlot());
            }
        }

        public void ShrinkSlots(int removedSlots)
        {
            if (CurrentSlotCount - removedSlots >= 0)
            {
                slots.RemoveRange(slots.Count - removedSlots, removedSlots);
            }
            else
            {
                throw new Exception("Cannot shrink inventory to negative slots");
            }
        }

        public int AddItem(InventoryItem item, int amount)
        {
            int remainingItems = amount;

            foreach (InventorySlot slot in slots)
            {
                if (slot.ItemIsNull)
                {
                    if (item.MaxAmount >= remainingItems)
                    {
                        slot.SetItem(item, remainingItems);
                        remainingItems = 0;
                        return remainingItems;
                    }
                    else
                    {
                        slot.SetItem(item, item.MaxAmount);
                        remainingItems -= item.MaxAmount;
                    }
                }
                else if (slot.CurrentItem == item)
                {
                    int spaceLeftInSlot = item.MaxAmount - slot.CurrentAmount;

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
            }

            return remainingItems;
        }

        public int SubtractItem(InventoryItem item, int amount)
        {
            int remainingItems = amount;

            for (int i = slots.Count - 1; i >= 0; i--)
            {
                InventorySlot slot = slots[i];

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

        public int SearchItem(InventoryItem item)
        {
            int totalAmount = 0;

            foreach (InventorySlot slot in slots)
            {
                if (slot.CurrentItem == item)
                {
                    totalAmount += slot.CurrentAmount;
                }
            }

            return totalAmount;
        }

        public void ExchangeItems(Inventory senderInventory, InventoryItem exchangedItem, int amount)
        {
            int remaining = AddItem(exchangedItem, amount);
            senderInventory.SubtractItem(exchangedItem, amount - remaining);
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
}