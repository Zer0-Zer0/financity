using System;
using UnityEngine;
using NUnit.Framework;

[TestFixture]
public class InventorySlotTests
{
    private InventorySlot slot;
    private InventoryItem item;

    [SetUp]
    public void Setup()
    {
        slot = new InventorySlot();
        item = ScriptableObject.CreateInstance<InventoryItem>();
        item.Name = "Test Item";
        item.MaxAmount = 10;
    }

    [Test]
    public void TestCurrentItem()
    {
        // Test setting a new item in an empty slot
        slot.CurrentItem = item;
        Assert.AreEqual(item, slot.CurrentItem);

        // Test changing item in the slot when current amount is not null
        Assert.DoesNotThrow(() => slot.CurrentItem = item, "Changed item in the inventory even though its amount is not null.");
    }

    [Test]
    public void TestCurrentAmount()
    {
        // Test setting a valid amount
        slot.CurrentAmount = 5;
        Assert.AreEqual(5, slot.CurrentAmount);

        // Test setting a negative amount
        Assert.Throws<Exception>(() => slot.CurrentAmount = -1, "Attempted to change item amount in the inventory to negative.");

        // Test setting an amount above the item's limit
        Assert.Throws<Exception>(() => slot.CurrentAmount = 15, "Attempted to change item amount in the inventory to above its limit.");

        // Test setting amount to 0
        slot.CurrentAmount = 0;
        Assert.IsNull(slot.CurrentItem);
    }
}
