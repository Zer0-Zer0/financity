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

        public Inventory(InventoryItem item, int amount, int initialSlotCount)
        {
            Inventory inventory = new Inventory(initialSlotCount);
            int remainingItems = amount;

            foreach (InventorySlot slot in inventory.slots)
            {
                if (remainingItems <= 0) break; // Exit if no remaining items

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

        public Inventory SubtractItem(Inventory sourceInventory)
        {
            Inventory remainingInventory = new Inventory(sourceInventory.CurrentSlotCount);

            foreach (InventorySlot slot in sourceInventory.slots)
            {
                if (slot.CurrentAmount > 0)
                {
                    InventorySlot remainingSlot = slot;

                    foreach (InventorySlot _slot in slots)
                    {
                        if (_slot.CurrentItem != remainingSlot.CurrentItem) continue;

                        if (_slot.CurrentAmount >= remainingSlot.CurrentAmount)
                        {
                            _slot.CurrentAmount -= remainingSlot.CurrentAmount;
                            remainingSlot.CurrentAmount = 0;
                            break;
                        }
                        else
                        {
                            remainingSlot.CurrentAmount -= _slot.CurrentAmount;
                            _slot.CurrentAmount = 0;

                            remainingInventory.AddItem(new Inventory(new List<InventorySlot> { remainingSlot }));
                        }
                    }
                }
            }

            return remainingInventory; // Return the inventory with remaining items
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