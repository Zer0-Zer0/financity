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
                throw new ArgumentOutOfRangeException(
                    nameof(initialSlotCount),
                    "Initial slot count cannot be negative."
                );
            ExpandSlots(initialSlotCount);
        }

        public Inventory(List<InventorySlot> initialSlots)
        {
            if (initialSlots == null)
                throw new ArgumentNullException(
                    nameof(initialSlots),
                    "Initial slots cannot be null."
                );
            slots = initialSlots;
        }

        public Inventory(InventoryItem item, int amount, int initialSlotCount)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            if (amount < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(amount),
                    "Amount must be greater than zero."
                );
            if (initialSlotCount <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(initialSlotCount),
                    "Initial slot count must be greater than zero."
                );

            if (amount == 0)
                ExpandSlots(0);
            else
            {
                ExpandSlots(initialSlotCount);
                AddItem(this, item, amount);
            }
        }

        public int GetCurrentSlotCount() => slots.Count;

        public List<InventorySlot> GetInventorySlots() => slots;

        public void ExpandSlots(int additionalSlots)
        {
            if (additionalSlots < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(additionalSlots),
                    "Additional slots cannot be negative."
                );
            for (int i = 0; i < additionalSlots; i++)
                slots.Add(new InventorySlot());
        }

        public void ShrinkSlots(int removedSlots)
        {
            if (removedSlots < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(removedSlots),
                    "Removed slots cannot be negative."
                );
            if (GetCurrentSlotCount() - removedSlots < 0)
                throw new Exception("Cannot shrink inventory to negative slots");
            slots.RemoveRange(slots.Count - removedSlots, removedSlots);
        }

        public static Inventory AddItem(Inventory inventory, Inventory items)
        {
            if (inventory == null)
                throw new ArgumentNullException(nameof(inventory), "Inventory cannot be null.");
            if (items == null)
                throw new ArgumentNullException(nameof(items), "Source inventory cannot be null.");
            if (items.GetCurrentSlotCount() <= 0)
                throw new ArgumentOutOfRangeException("Source inventory is empty.");

            Inventory remainingInventory = new Inventory(items.GetCurrentSlotCount());

            foreach (InventorySlot slot in items.slots)
                if (slot.CurrentAmount > 0)
                    remainingInventory = AddItem(inventory, slot);

            return remainingInventory;
        }

        public static Inventory AddItem(Inventory inventory, InventorySlot slot)
        {
            if (inventory == null)
                throw new ArgumentNullException(nameof(inventory), "Inventory cannot be null.");
            if (slot == null)
                throw new ArgumentNullException(nameof(slot), "Slot cannot be null.");
            if (slot.CurrentAmount <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(slot.CurrentAmount),
                    "Slot amount must be greater than zero."
                );

            Inventory remainingInventory = AddItem(inventory, slot.CurrentItem, slot.CurrentAmount);
            return remainingInventory;
        }

        public static Inventory AddItem(Inventory inventory, InventoryItem item, int amount)
        {
            if (inventory == null)
                throw new ArgumentNullException(nameof(inventory), "Inventory cannot be null.");
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(amount),
                    "Amount must be greater than zero."
                );
            else if (amount == 0)
                return new Inventory();

            int remainingItems = amount;

            foreach (InventorySlot slot in inventory.slots)
            {
                if (remainingItems <= 0)
                    break;

                if (slot.ItemIsNull)
                    remainingItems = inventory.AddToEmptySlot(slot, item, remainingItems);
                else if (slot.CurrentItem == item)
                    remainingItems = inventory.AddToExistingSlot(slot, item, remainingItems);
            }

            Inventory remainingItemInventory = new Inventory(
                item,
                remainingItems,
                inventory.GetCurrentSlotCount()
            );
            return remainingItemInventory;
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

        public static Inventory SubtractItem(Inventory inventory, Inventory items)
        {
            if (inventory == null)
                throw new ArgumentNullException(nameof(inventory), "Inventory cannot be null.");
            if (items == null)
                throw new ArgumentNullException(nameof(items), "Source inventory cannot be null.");
            if (items.GetCurrentSlotCount() <= 0)
                throw new InvalidOperationException("Source inventory is empty.");

            Inventory remainingInventory = new Inventory(items.GetCurrentSlotCount());

            foreach (InventorySlot slot in items.slots)
                if (slot.CurrentAmount > 0)
                    AddItem(remainingInventory, SubtractItem(inventory, slot));

            return remainingInventory;
        }

        public static Inventory SubtractItem(Inventory inventory, InventorySlot slot)
        {
            if (inventory == null)
                throw new ArgumentNullException(nameof(inventory), "Inventory cannot be null.");
            if (slot == null)
                throw new ArgumentNullException(nameof(slot), "Slot cannot be null.");

            Inventory remainingInventory = SubtractItem(
                inventory,
                slot.CurrentItem,
                slot.CurrentAmount
            );
            return remainingInventory;
        }

        public static Inventory SubtractItem(Inventory inventory, InventoryItem item, int amount)
        {
            if (inventory == null)
                throw new ArgumentNullException(nameof(inventory), "Inventory cannot be null.");
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(amount),
                    "Amount must be greater than zero."
                );

            int totalAvailable = inventory.SearchItem(item);

            int remainingItems = amount;

            for (int i = inventory.slots.Count - 1; i >= 0; i--)
            {
                InventorySlot slot = inventory.slots[i];

                if (slot.CurrentItem == item)
                {
                    remainingItems = inventory.SubtractFromSlot(slot, remainingItems);
                    if (remainingItems == 0)
                        break;
                }
            }

            Inventory remainingInventory = new Inventory(
                item,
                remainingItems,
                inventory.GetCurrentSlotCount()
            );
            return remainingInventory;
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

        public static void ExchangeItems(
            Inventory inventory,
            Inventory senderInventory,
            Inventory exchangedItems
        )
        {
            if (inventory == null)
                throw new ArgumentNullException(nameof(inventory), "Inventory cannot be null.");
            if (senderInventory == null)
                throw new ArgumentNullException(
                    nameof(senderInventory),
                    "Sender inventory cannot be null."
                );
            if (exchangedItems == null)
                throw new ArgumentNullException(
                    nameof(exchangedItems),
                    "Exchanged items cannot be null."
                );

            Inventory remainingItems = AddItem(inventory, exchangedItems);
            Inventory removeFromSender = SubtractItem(exchangedItems, remainingItems);
            SubtractItem(senderInventory, removeFromSender);
        }

        public static void ExchangeItems(
            Inventory inventory,
            Inventory senderInventory,
            InventoryItem exchangedItem,
            int amount
        )
        {
            if (inventory == null)
                throw new ArgumentNullException(nameof(inventory), "Inventory cannot be null.");
            if (senderInventory == null)
                throw new ArgumentNullException(
                    nameof(senderInventory),
                    "Sender inventory cannot be null."
                );
            if (exchangedItem == null)
                throw new ArgumentNullException(
                    nameof(exchangedItem),
                    "Exchanged item cannot be null."
                );
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(amount),
                    "Amount must be greater than zero."
                );

            Inventory remainingItems = AddItem(inventory, exchangedItem, amount);
            SubtractItem(senderInventory, remainingItems);
        }

        public override string ToString()
        {
            string result = "Inventory Slots:\n";
            foreach (InventorySlot slot in slots)
                if (slot.CurrentItem != null)
                    result +=
                        slot.CurrentItem.ToString()
                        + ", Current Amount: "
                        + slot.CurrentAmount
                        + "\n";
            return result;
        }
    }
}
