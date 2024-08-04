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
        Assert.AreEqual(new Inventory(0).ToString(), inventory.AddItem(sourceInventory)); // Add source inventory to the main inventory

        // Test adding more items than slot capacity
        sourceInventory.AddItem(new Inventory(SlotListFactory(new InventorySlot(banana, 5)))); // Add more bananas to source inventory
        Assert.AreEqual(3, inventory.AddItem(sourceInventory)); // Check remaining items after adding
    }

    [Test]
    public void CanSubtractItem()
    {
        List<InventorySlot> slotList = SlotListFactory(new InventorySlot(banana, 8));
        slotList.Add(new InventorySlot(apple, 5));
        // Create a new inventory for adding items
        Inventory sourceInventory = new Inventory(slotList);

        // Test removing items
        Assert.AreEqual(new Inventory(0).ToString(), inventory.SubtractItem(sourceInventory)); // Subtract from main inventory
        //Assert.AreEqual(2, inventory.SubtractItem(sourceInventory)); // Check remaining items
    }

    [Test]
    public void CanSearchItem()
    {
        List<InventorySlot> slotList = SlotListFactory(new InventorySlot(banana, 28));
        slotList.Add(new InventorySlot(apple, 6));
        // Create a new inventory for adding items
        Inventory sourceInventory = new Inventory(slotList);

        // Test searching for items
        Assert.AreEqual(28, inventory.SearchItem(banana)); // Search for bananas
        Assert.AreEqual(6, inventory.SearchItem(apple)); // Search for apples
        Assert.AreEqual(0, inventory.SearchItem(ItemFactory())); // Search for a non-existent item
    }

    [Test]
    public void CanExchangeItems()
    {
        List<InventorySlot> slotList = SlotListFactory(new InventorySlot(banana, 5));
        // Create a second inventory for exchange testing
        Inventory _senderInventory = new Inventory(slotList);

        // Prepare items to exchange
        List<InventorySlot> slotList2 = SlotListFactory(new InventorySlot(banana, 3));
        Inventory exchangedItems = new Inventory(slotList2);

        // Test exchanging items between inventories
        inventory.ExchangeItems(_senderInventory, exchangedItems); // Perform exchange

        // Check if items were exchanged correctly
        Assert.AreEqual(3, inventory.SearchItem(banana)); // Check bananas in main inventory
        Assert.AreEqual(2, _senderInventory.SearchItem(banana)); // Check remaining bananas in sender inventory
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