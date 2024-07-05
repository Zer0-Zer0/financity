using System;
using UnityEngine;
using NUnit.Framework;

[TestFixture]
public class InventoryTests
{
    private GameObject testObject;
    private Inventory inventory;
    private InventoryItem item1;
    private InventoryItem item2;

    [SetUp]
    public void Setup()
    {
        testObject = new GameObject();
        inventory = testObject.AddComponent<Inventory>();
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
    }

    [Test]
    public void TestAddItem()
    {
        // Test adding items to empty slots
        Assert.AreEqual(0, inventory.AddItem(item1, 5));
        Assert.AreEqual(0, inventory.AddItem(item2, 3));

        // Test adding items to slots with existing items
        Assert.AreEqual(0, inventory.AddItem(item1, 3));
        Assert.AreEqual(0, inventory.AddItem(item2, 4));

        // Test adding more items than slot capacity
        Assert.AreEqual(3, inventory.AddItem(item1, 25));
    }

    [Test]
    public void TestRemoveItem()
    {
        // Add items to slots for removal testing
        inventory.AddItem(item1, 8);
        inventory.AddItem(item2, 5);

        // Test removing items
        Assert.AreEqual(0, inventory.RemoveItem(item1, 5));
        Assert.AreEqual(2, inventory.RemoveItem(item1, 5));

        Assert.AreEqual(0, inventory.RemoveItem(item2, 4));
        Assert.AreEqual(4, inventory.RemoveItem(item2, 5));
    }

    [Test]
    public void TestToString()
    {
        // Add items to slots for testing ToString method
        inventory.AddItem(item1, 8);

        string expectedString = "Inventory Slots:\nItem: Item1, Max Amount: 10, Current Amount: 8\n";
        string actualString = inventory.ToString();
        Assert.IsTrue(actualString.Contains(expectedString));
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(testObject);
    }
}
