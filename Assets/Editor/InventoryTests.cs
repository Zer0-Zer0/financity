using System;
using UnityEngine;
using NUnit.Framework;

[TestFixture]
public class InventoryTests
{
    private Inventory inventory;
    private InventoryItem item1;
    private InventoryItem item2;

    [SetUp]
    public void Setup()
    {
        inventory = new Inventory();
        inventory.slots = new InventorySlot[5];
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            inventory.slots[i] = new InventorySlot();
        }

        item1 = ScriptableObject.CreateInstance<InventoryItem>();
        item1.Name = "Item1";
        item1.MaxAmount = 10;

        item2 = ScriptableObject.CreateInstance<InventoryItem>();
        item2.Name = "Item2";
        item2.MaxAmount = 5;

        Debug.Log(item1);
        Debug.Log(item2);
    }

    [Test]
    public void TestAddItem()
    {
        // Test adding items to empty slots
        Assert.AreEqual(0, inventory.AddItem(item1, 5)); // Add 5 items of item1
        Assert.AreEqual(0, inventory.AddItem(item2, 3)); // Add 3 items of item2

        // Test adding items to slots with existing items
        Assert.AreEqual(0, inventory.AddItem(item1, 7)); // Add 7 more items of item1
        Assert.AreEqual(2, inventory.AddItem(item2, 4)); // Add 4 items of item2 (2 items remaining)

        // Test adding more items than slot capacity
        Assert.AreEqual(3, inventory.AddItem(item1, 20)); // Add 20 items of item1 (3 items remaining)
    }

    [Test]
    public void TestRemoveItem()
    {
        // Add items to slots for removal testing
        inventory.AddItem(item1, 8);
        inventory.AddItem(item2, 4);

        // Test removing items
        Assert.AreEqual(0, inventory.RemoveItem(item1, 5)); // Remove 5 items of item1
        Assert.AreEqual(3, inventory.RemoveItem(item1, 5)); // Remove 5 more items of item1 (3 items remaining)

        Assert.AreEqual(0, inventory.RemoveItem(item2, 4)); // Remove all items of item2
        Assert.AreEqual(4, inventory.RemoveItem(item2, 5)); // Try to remove more items than available (4 items remaining)
    }

    [Test]
    public void TestToString()
    {
        // Add items to slots for testing ToString method
        inventory.AddItem(item1, 8);
        inventory.AddItem(item2, 4);

        string expectedString = "Inventory Slots:\nItem1 - 8\nItem2 - 4\n";
        Assert.AreEqual(expectedString, inventory.ToString());
    }
}
