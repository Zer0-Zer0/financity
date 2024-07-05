using System;
using UnityEngine;
using NUnit.Framework;

[TestFixture]
public class InventorySlotTests
{
    private InventorySlot slot;
    private InventoryItem item1;
    private InventoryItem item2;

    [SetUp]
    public void Setup()
    {
        slot = new InventorySlot();
        item1 = ScriptableObject.CreateInstance<InventoryItem>();
        item1.Name = "Item1";
        item1.MaxAmount = 10;

        item2 = ScriptableObject.CreateInstance<InventoryItem>();
        item2.Name = "Item2";
        item2.MaxAmount = 5;

        slot.AddItem(item1, 5);
    }

    [Test]
    public void TestCurrentItem()
    {
        // Test setting a new item in an empty slot
        Assert.AreEqual(item1, slot.CurrentItem);

        // Test changing item to a different one
        Assert.That(() => slot.CurrentItem = item2, Throws.TypeOf<Exception>());
    }

    [Test]
    public void TestCurrentAmount()
    {
        // Test setting a valid amount
        Assert.AreEqual(5, slot.CurrentAmount);

        // Test setting a negative amount
        Assert.That(() => slot.CurrentAmount = -1, Throws.TypeOf<Exception>());

        // Test setting an amount above the item's limit
        Assert.That(() => slot.CurrentAmount = 15, Throws.TypeOf<Exception>());

        slot.ToString();
        // Test setting amount to 0
        slot.CurrentAmount = 0;
        Assert.IsNull(slot.CurrentItem);
    }
}