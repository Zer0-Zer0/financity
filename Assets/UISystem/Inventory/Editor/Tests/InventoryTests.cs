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
        testObject = new GameObject();
        Inventory _inventory = new Inventory(slotCount);

        return _inventory;
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
        // Test adding items to empty slots
        Assert.AreEqual(0, inventory.AddItem(new InventorySlot(banana, 5)));
        Assert.AreEqual(0, inventory.AddItem(new InventorySlot(apple, 3)));

        // Test adding items to slots with existing items
        Assert.AreEqual(0, inventory.AddItem(new InventorySlot(banana, 3)));
        Assert.AreEqual(0, inventory.AddItem(new InventorySlot(apple, 4)));

        // Test adding more items than slot capacity
        Assert.AreEqual(3, inventory.AddItem(new InventorySlot(banana, 25)));
    }

    [Test]
    public void CanSubtractItem()
    {
        // Add items to slots for removal testing
        inventory.AddItem(new InventorySlot(banana, 8));
        inventory.AddItem(new InventorySlot(apple, 5));

        // Test removing items
        Assert.AreEqual(0, inventory.SubtractItem(new InventorySlot(banana, 5)));
        Assert.AreEqual(2, inventory.SubtractItem(new InventorySlot(banana, 5)));

        Assert.AreEqual(0, inventory.SubtractItem(new InventorySlot(apple, 4)));
        Assert.AreEqual(4, inventory.SubtractItem(new InventorySlot(apple, 5)));
    }

    [Test]
    public void CanSearchItem()
    {
        inventory.AddItem(new InventorySlot(banana, 28));
        inventory.AddItem(new InventorySlot(apple, 6));

        // Test searching for items
        Assert.AreEqual(28, inventory.SearchItem(banana));
        Assert.AreEqual(6, inventory.SearchItem(apple));
        Assert.AreEqual(0, inventory.SearchItem(ItemFactory()));
    }

    [Test]
    public void CanExchangeItems()
    {
        // Create a second inventory for exchange testing
        Inventory _senderInventory = InventoryFactory();

        // Add items to sender inventory for exchange
        _senderInventory.AddItem(new InventorySlot(banana, 5));

        // Test exchanging items between inventories
        inventory.ExchangeItems(_senderInventory, new InventorySlot(banana, 3));

        // Check if items were exchanged correctly
        Assert.AreEqual(3, inventory.SearchItem(banana));
        Assert.AreEqual(2, _senderInventory.SearchItem(banana));
    }

    [Test]
    public void CanToString()
    {
        // Add items to slots for testing ToString method
        inventory.AddItem(new InventorySlot(banana, 8));

        string expectedString = "Inventory Slots:\nItem: banana, Max Amount: 10, Current Amount: 8\n";
        string actualString = inventory.ToString();
        Assert.IsTrue(actualString.Contains(expectedString));
    }

    [TearDown]
    public void TearDown()
    {
        inventory = null;
        banana = null;
        apple = null;
        GameObject.DestroyImmediate(testObject);
    }
}
