using System;
using System.Collections.Generic;
using UnityEngine;

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

        public Inventory(InventoryItem item, int amount, int initialSlotCount)
        {
            ExpandSlots(initialSlotCount);
            int remainingItems = amount;

            foreach (InventorySlot slot in slots)
            {
                if (remainingItems <= 0) break;
                if (slot.ItemIsNull)
                {
                    int itemsToAdd = Math.Min(item.MaxAmount, remainingItems);
                    slot.SetItem(item, itemsToAdd);
                    remainingItems -= itemsToAdd;
                }
                else if (slot.CurrentItem == item)
                {
                    int spaceLeftInSlot = item.MaxAmount - slot.CurrentAmount;
                    int itemsToAdd = Math.Min(spaceLeftInSlot, remainingItems);
                    slot.CurrentAmount += itemsToAdd;
                    remainingItems -= itemsToAdd;
                }
            }
        }

        public int CurrentSlotCount => slots.Count;

        public void ExpandSlots(int additionalSlots)
        {
            for (int i = 0; i < additionalSlots; i++)
            {
                slots.Add(new InventorySlot());
            }
        }

        public void ShrinkSlots(int removedSlots)
        {
            if (CurrentSlotCount - removedSlots < 0)
            {
                throw new Exception("Cannot shrink inventory to negative slots");
            }
            slots.RemoveRange(slots.Count - removedSlots, removedSlots);
        }

        public Inventory AddItem(Inventory sourceInventory)
        {
            Inventory remainingInventory = new Inventory(sourceInventory.CurrentSlotCount);

            foreach (InventorySlot slot in sourceInventory.slots)
            {
                if (slot.CurrentAmount > 0)
                {
                    AddItemToInventory(slot, remainingInventory);
                }
            }

            return remainingInventory;
        }

        private void AddItemToInventory(InventorySlot slot, Inventory remainingInventory)
        {
            InventoryItem item = slot.CurrentItem;
            int remainingItems = slot.CurrentAmount;

            foreach (InventorySlot _slot in slots)
            {
                if (remainingItems <= 0) break;

                if (_slot.ItemIsNull)
                {
                    int amountToAdd = Math.Min(item.MaxAmount, remainingItems);
                    _slot.SetItem(item, amountToAdd);
                    remainingItems -= amountToAdd;
                }
                else if (_slot.CurrentItem == item)
                {
                    int spaceLeftInSlot = item.MaxAmount - _slot.CurrentAmount;
                    int amountToAdd = Math.Min(spaceLeftInSlot, remainingItems);
                    _slot.CurrentAmount += amountToAdd;
                    remainingItems -= amountToAdd;
                }
            }
        }

        public int AddItem(InventoryItem item, int amount)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            }
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
            }

            int remainingItems = amount;

            foreach (InventorySlot slot in slots)
            {
                if (remainingItems <= 0) break;

                if (slot.ItemIsNull)
                {
                    remainingItems = AddToEmptySlot(slot, item, remainingItems);
                }
                else if (slot.CurrentItem == item)
                {
                    remainingItems = AddToExistingSlot(slot, item, remainingItems);
                }
            }

            return remainingItems;
        }

        private int AddToEmptySlot(InventorySlot slot, InventoryItem item, int remainingItems)
        {
            if (item.MaxAmount >= remainingItems)
            {
                slot.SetItem(item, remainingItems);
                return 0;
            }
            else
            {
                slot.SetItem(item, item.MaxAmount);
                return remainingItems - item.MaxAmount;
            }
        }

        private int AddToExistingSlot(InventorySlot slot, InventoryItem item, int remainingItems)
        {
            int spaceLeftInSlot = item.MaxAmount - slot.CurrentAmount;

            if (spaceLeftInSlot >= remainingItems)
            {
                slot.CurrentAmount += remainingItems;
                return 0;
            }
            else
            {
                slot.CurrentAmount = item.MaxAmount;
                return remainingItems - spaceLeftInSlot;
            }
        }

        public Inventory SubtractItem(Inventory sourceInventory)
        {
            Inventory remainingInventory = new Inventory(sourceInventory.CurrentSlotCount);

            foreach (InventorySlot slot in sourceInventory.slots)
            {
                if (slot.CurrentAmount > 0)
                {
                    SubtractItemFromInventory(slot, remainingInventory);
                }
            }

            return remainingInventory;
        }

        private void SubtractItemFromInventory(InventorySlot slot, Inventory remainingInventory)
        {
            foreach (InventorySlot _slot in slots)
            {
                if (_slot.CurrentItem != slot.CurrentItem) continue;

                if (_slot.CurrentAmount >= slot.CurrentAmount)
                {
                    _slot.CurrentAmount -= slot.CurrentAmount;
                    slot.CurrentAmount = 0;
                    break;
                }
                else
                {
                    slot.CurrentAmount -= _slot.CurrentAmount;
                    _slot.CurrentAmount = 0;
                    remainingInventory.AddItem(new Inventory(new List<InventorySlot> { slot }));
                }
            }
        }

        public int SubtractItem(InventoryItem item, int amount)
        {
            int remainingItems = amount;

            for (int i = slots.Count - 1; i >= 0; i--)
            {
                InventorySlot slot = slots[i];

                if (slot.CurrentItem == item)
                {
                    remainingItems = SubtractFromSlot(slot, remainingItems);
                    if (remainingItems == 0) break;
                }
            }

            return remainingItems;
        }

        private int SubtractFromSlot(InventorySlot slot, int remainingItems)
        {
            if (slot.CurrentAmount >= remainingItems)
            {
                slot.CurrentAmount -= remainingItems;
                return 0;
            }
            else
            {
                remainingItems -= slot.CurrentAmount;
                slot.CurrentAmount = 0;
                return remainingItems;
            }
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