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
    }

    [Test]
    public void TestAddItem()
    {
        //Trying to Add too much items to a slot
        Assert.That(() => slot.AddItem(item2, 6), Throws.TypeOf<Exception>());

        //Trying to negative items to a slot
        Assert.That(() => slot.AddItem(item2, -1), Throws.TypeOf<Exception>());

        // Test adding an item to an empty slot
        slot.AddItem(item2, 3);

        //Sanity check
        Assert.AreEqual(item2, slot.CurrentItem);
        Assert.AreEqual(3, slot.CurrentAmount);

        // Test adding a different item to a non-empty slot
        Assert.That(() => slot.AddItem(item1, 2), Throws.TypeOf<Exception>());
    }

    [Test]
    public void TestCurrentItem()
    {
        // Test setting a new item in an empty slot
        slot.AddItem(item1, 5);

        // Test changing item to the same (Should not Throw)
        Assert.DoesNotThrow(() => slot.CurrentItem = item1);

        // Test changing item to a different one (Should Throw)
        Assert.That(() => slot.CurrentItem = item2, Throws.TypeOf<Exception>());
    }

    [Test]
    public void TestCurrentAmount()
    {
        slot.AddItem(item1, 5);

        // Test setting a negative amount
        Assert.That(() => slot.CurrentAmount = -1, Throws.TypeOf<Exception>());

        // Test setting an amount above the item's limit
        Assert.That(() => slot.CurrentAmount = 15, Throws.TypeOf<Exception>());

        // Test setting amount to 0
        slot.CurrentAmount = 0;
        Assert.DoesNotThrow(() => slot.CurrentAmount = 0);
        Assert.IsNull(slot.CurrentItem);
    }
}