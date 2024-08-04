using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UISystem
{
    public class Inventory
    {
        public List<InventorySlot> slots { get; private set; } = new List<InventorySlot>();

        public Inventory(int initialSlotCount)
        {
            ExpandSlots(initialSlotCount);
        }

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

        public int AddItem(InventorySlot slot)
        {
            int remainingItems = slot.CurrentAmount;
            InventoryItem item = slot.CurrentItem;

            foreach (InventorySlot _slot in slots)
            {
                if (_slot.ItemIsNull)
                {
                    int amountToAdd = Math.Min(item.MaxAmount, remainingItems);
                    _slot.SetItem(item, amountToAdd);
                    remainingItems -= amountToAdd;

                    if (remainingItems == 0)
                        return 0;
                }
                else if (_slot.CurrentItem == item)
                {
                    int spaceLeftInSlot = item.MaxAmount - _slot.CurrentAmount;
                    int amountToAdd = Math.Min(spaceLeftInSlot, remainingItems);
                    _slot.CurrentAmount += amountToAdd;
                    remainingItems -= amountToAdd;

                    if (remainingItems == 0)
                        return 0;
                }
            }

            return remainingItems;
        }

        public int SubtractItem(InventorySlot slot)
        {
            int remainingItems = slot.CurrentAmount;
            InventoryItem item = slot.CurrentItem;

            foreach (InventorySlot _slot in slots)
            {
                if (_slot.CurrentItem != item) continue;

                if (_slot.CurrentAmount >= remainingItems)
                {
                    _slot.CurrentAmount -= remainingItems;
                    return 0;
                }
                else
                {
                    remainingItems -= _slot.CurrentAmount;
                    _slot.CurrentAmount = 0;
                }
            }

            return remainingItems;
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

        public void ExchangeItems(Inventory senderInventory, InventorySlot exchangedItem)
        {
            int remaining = AddItem(exchangedItem);
            InventorySlot removeFromSender = new InventorySlot(exchangedItem.CurrentItem, exchangedItem.CurrentAmount - remaining);
            senderInventory.SubtractItem(removeFromSender);
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