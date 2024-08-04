using System.Collections.Generic;
using System;
using UnityEngine;
using NUnit.Framework;
using UISystem;

[TestFixture]
public class InventoryTests
{
    private GameObject testObject;
    private Inventory inventory;
    private InventoryItem banana, apple;

    [SetUp]
    public void Setup()
    {
        testObject = new GameObject();
        inventory = InventoryFactory();

        banana = ItemFactory("banana");
        apple = ItemFactory("apple", 5);
    }

    private Inventory InventoryFactory(int slotCount = 5)
    {
        return new Inventory(slotCount);
    }

    private List<InventorySlot> SlotListFactory(InventorySlot item)
    {
        List<InventorySlot> slotList = new List<InventorySlot>() { item };
        return slotList;
    }

    private InventoryItem ItemFactory(string name = "Item", int maxAmount = 10)
    {
        InventoryItem _item = ScriptableObject.CreateInstance<InventoryItem>();
        _item.Name = name;
        _item.MaxAmount = maxAmount;
        return _item;
    }

    [Test]
    public void CanExpandSlots()
    {
        int initialSlotCount = inventory.slots.Count;
        int additionalSlots = 3;

        inventory.ExpandSlots(additionalSlots);

        Assert.AreEqual(initialSlotCount + additionalSlots, inventory.slots.Count);
    }

    [Test]
    public void CanShrinkSlots()
    {
        int initialSlotCount = inventory.slots.Count;
        int removedSlots = 2;

        inventory.ShrinkSlots(removedSlots);

        Assert.AreEqual(initialSlotCount - removedSlots, inventory.slots.Count);
    }

    [Test]
    public void CanAddItem()
    {
        List<InventorySlot> slotList = SlotListFactory(new InventorySlot(banana, 5));
        slotList.Add(new InventorySlot(apple, 3));
        // Create a new inventory for adding items
        Inventory sourceInventory = new Inventory(slotList);

        // Test adding items to the inventory
        Assert.AreEqual(new Inventory(0).ToString(), inventory.AddItem(sourceInventory).ToString()); // Add source inventory to the main inventory

        // Test adding more items than slot capacity
        Assert.AreEqual(5, inventory.SearchItem(banana)); // Check remaining items after adding
    }

    [Test]
    public void CanSubtractItem()
    {
        List<InventorySlot> slotList = SlotListFactory(new InventorySlot(banana, 8));
        slotList.Add(new InventorySlot(apple, 5));
        // Create a new inventory for adding items
        Inventory sourceInventory = new Inventory(slotList);

        // Test removing items
        Assert.AreEqual(new Inventory(0).ToString(), inventory.SubtractItem(sourceInventory).ToString()); // Subtract from main inventory
        //Assert.AreEqual(2, inventory.SubtractItem(sourceInventory)); // Check remaining items
    }

    [Test]
    public void CanSearchItem()
    {
        Inventory sourceInventory = new Inventory(banana, 28, 10);
        sourceInventory.AddItem(apple, 6);

        // Test searching for items
        Assert.AreEqual(28, sourceInventory.SearchItem(banana));
        Assert.AreEqual(6, sourceInventory.SearchItem(apple));
        Assert.AreEqual(0, sourceInventory.SearchItem(ItemFactory()));
    }

    [Test]
    public void CanExchangeItems()
    {
        Inventory _senderInventory = new Inventory(banana, 5, 1);
        inventory.ExchangeItems(_senderInventory, banana, 3);

        // Check if items were exchanged correctly
        Assert.AreEqual(3, inventory.SearchItem(banana));
        Assert.AreEqual(2, _senderInventory.SearchItem(banana));
    }

    [Test]
    public void CanToString()
    {
        // Add items to slots for testing ToString method
        inventory.AddItem(new Inventory(SlotListFactory(new InventorySlot(banana, 8)))); // Add bananas to main inventory

        string expectedString = "Inventory Slots:\nItem: banana, Max Amount: 10, Current Amount: 8\n";
        string actualString = inventory.ToString(); // Get string representation of inventory
        Assert.IsTrue(actualString.Contains(expectedString)); // Check if expected string is in actual string
    }

    [TearDown]
    public void TearDown()
    {
        inventory = null;
        banana = null;
        apple = null;
        GameObject.DestroyImmediate(testObject); // Clean up test object
    }
}