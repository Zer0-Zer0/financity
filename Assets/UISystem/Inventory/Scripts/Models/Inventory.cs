using System;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    [System.Serializable]
    public class Inventory
    {
        private List<InventorySlot> slots = new List<InventorySlot>();

        public Inventory(int initialSlotCount = 0)
        {
            if (initialSlotCount < 0)
                throw new ArgumentOutOfRangeException(nameof(initialSlotCount), "Initial slot count cannot be negative.");
            ExpandSlots(initialSlotCount);
        }

        public Inventory(List<InventorySlot> initialSlots)
        {
            if (initialSlots == null)
                throw new ArgumentNullException(nameof(initialSlots), "Initial slots cannot be null.");
            slots = initialSlots;
        }

        public Inventory(InventoryItem item, int amount, int initialSlotCount)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
            if (initialSlotCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(initialSlotCount), "Initial slot count must be greater than zero.");

            ExpandSlots(initialSlotCount);
            int remainingItems = amount;
            AddItem(item, amount);
        }

        public int GetCurrentSlotCount() => slots.Count;

        public List<InventorySlot> GetInventorySlots() => slots;

        public void ExpandSlots(int additionalSlots)
        {
            if (additionalSlots < 0)
                throw new ArgumentOutOfRangeException(nameof(additionalSlots), "Additional slots cannot be negative.");
            for (int i = 0; i < additionalSlots; i++)
                slots.Add(new InventorySlot());
        }

        public void ShrinkSlots(int removedSlots)
        {
            if (removedSlots < 0)
                throw new ArgumentOutOfRangeException(nameof(removedSlots), "Removed slots cannot be negative.");
            if (GetCurrentSlotCount() - removedSlots < 0)
                throw new Exception("Cannot shrink inventory to negative slots");
            slots.RemoveRange(slots.Count - removedSlots, removedSlots);
        }

        public Inventory AddItem(Inventory sourceInventory)
        {
            if (sourceInventory == null)
                throw new ArgumentNullException(nameof(sourceInventory), "Source inventory cannot be null.");

            Inventory remainingInventory = new Inventory(sourceInventory.GetCurrentSlotCount());

            foreach (InventorySlot slot in sourceInventory.slots)
                if (slot.CurrentAmount > 0)
                    AddItemToInventory(slot, remainingInventory);

            return remainingInventory;
        }

        private void AddItemToInventory(InventorySlot slot, Inventory remainingInventory)
        {
            if (slot == null)
                throw new ArgumentNullException(nameof(slot), "Slot cannot be null.");
            if (remainingInventory == null)
                throw new ArgumentNullException(nameof(remainingInventory), "Remaining inventory cannot be null.");

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

            remainingInventory.AddItem(slot.CurrentItem, remainingItems);
        }

        public int AddItem(InventoryItem item, int amount)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
            else if (amount == 0)
                return 0;

            int remainingItems = amount;

            foreach (InventorySlot slot in slots)
            {
                if (remainingItems <= 0) break;

                if (slot.ItemIsNull)
                    remainingItems = AddToEmptySlot(slot, item, remainingItems);
                else if (slot.CurrentItem == item)
                    remainingItems = AddToExistingSlot(slot, item, remainingItems);
            }

            return remainingItems;
        }

        private int AddToEmptySlot(InventorySlot slot, InventoryItem item, int remainingItems)
        {
            if (slot == null)
                throw new ArgumentNullException(nameof(slot), "Slot cannot be null.");
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");

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
            if (slot == null)
                throw new ArgumentNullException(nameof(slot), "Slot cannot be null.");
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");

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
            if (sourceInventory == null)
                throw new ArgumentNullException(nameof(sourceInventory), "Source inventory cannot be null.");

            Inventory remainingInventory = new Inventory(sourceInventory.GetCurrentSlotCount());

            foreach (InventorySlot slot in sourceInventory.slots)
                if (slot.CurrentAmount > 0)
                    SubtractItemFromInventory(slot, remainingInventory);

            return remainingInventory;
        }

        private void SubtractItemFromInventory(InventorySlot slot, Inventory remainingInventory)
        {
            if (slot == null)
                throw new ArgumentNullException(nameof(slot), "Slot cannot be null.");
            if (remainingInventory == null)
                throw new ArgumentNullException(nameof(remainingInventory), "Remaining inventory cannot be null.");


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
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");

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
            if (slot == null)
                throw new ArgumentNullException(nameof(slot), "Slot cannot be null.");

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
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");

            int totalAmount = 0;

            foreach (InventorySlot slot in slots)
                if (slot.CurrentItem == item)
                    totalAmount += slot.CurrentAmount;

            return totalAmount;
        }

        public void ExchangeItems(Inventory senderInventory, Inventory exchangedItems)
        {
            if (senderInventory == null)
                throw new ArgumentNullException(nameof(senderInventory), "Sender inventory cannot be null.");
            if (exchangedItems == null)
                throw new ArgumentNullException(nameof(exchangedItems), "Exchanged items cannot be null.");

            Inventory remainingItems = AddItem(exchangedItems);
            Inventory removeFromSender = exchangedItems.SubtractItem(remainingItems);
            senderInventory.SubtractItem(removeFromSender);
        }

        public void ExchangeItems(Inventory senderInventory, InventoryItem exchangedItem, int amount)
        {
            if (senderInventory == null)
                throw new ArgumentNullException(nameof(senderInventory), "Sender inventory cannot be null.");
            if (exchangedItem == null)
                throw new ArgumentNullException(nameof(exchangedItem), "Exchanged item cannot be null.");
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");

            int remaining = AddItem(exchangedItem, amount);
            senderInventory.SubtractItem(exchangedItem, amount - remaining);
        }

        public override string ToString()
        {
            string result = "Inventory Slots:\n";
            foreach (InventorySlot slot in slots)
                if (slot.CurrentItem != null)
                    result += slot.CurrentItem.ToString() + ", Current Amount: " + slot.CurrentAmount + "\n";
            return result;
        }
    }
}