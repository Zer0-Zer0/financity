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

        public Inventory(List<InventorySlot> initialSlots)
        {
            slots = initialSlots ?? new List<InventorySlot>();
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

        public Inventory AddItem(Inventory sourceInventory)
        {
            Inventory remainingInventory = new Inventory(sourceInventory.CurrentSlotCount);

            foreach (InventorySlot slot in sourceInventory.slots)
            {
                if (slot.CurrentAmount > 0)
                {
                    InventoryItem item = slot.CurrentItem;
                    int remainingItems = slot.CurrentAmount;

                    foreach (InventorySlot _slot in slots)
                    {
                        if (_slot.ItemIsNull)
                        {
                            int amountToAdd = Math.Min(item.MaxAmount, remainingItems);
                            _slot.SetItem(item, amountToAdd);
                            remainingItems -= amountToAdd;

                            if (remainingItems == 0)
                                break;
                        }
                        else if (_slot.CurrentItem == item)
                        {
                            int spaceLeftInSlot = item.MaxAmount - _slot.CurrentAmount;
                            int amountToAdd = Math.Min(spaceLeftInSlot, remainingItems);
                            _slot.CurrentAmount += amountToAdd;
                            remainingItems -= amountToAdd;

                            if (remainingItems == 0)
                                break;
                        }
                    }
                }
            }

            return remainingInventory;
        }

        public Inventory SubtractItem(Inventory sourceInventory)
        {
            Inventory remainingInventory = sourceInventory;

            foreach (InventorySlot slot in sourceInventory.slots)
            {
                if (slot.CurrentAmount > 0)
                {
                    InventoryItem item = slot.CurrentItem;
                    int remainingItems = slot.CurrentAmount;

                    foreach (InventorySlot _slot in slots)
                    {
                        if (_slot.CurrentItem != item) continue;

                        if (_slot.CurrentAmount >= remainingItems)
                        {
                            _slot.CurrentAmount -= remainingItems;
                            remainingItems = 0;
                            break;
                        }
                        else
                        {
                            remainingItems -= _slot.CurrentAmount;
                            _slot.CurrentAmount = 0;
                        }
                    }
                }
            }

            return remainingInventory; // Return the inventory with remaining items
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

        public void ExchangeItems(Inventory senderInventory, Inventory exchangedItems)
        {
            Inventory remainingItems = AddItem(exchangedItems);
            Inventory removeFromSender = exchangedItems.SubtractItem(remainingItems);
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