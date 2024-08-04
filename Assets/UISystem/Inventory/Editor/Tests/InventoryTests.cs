using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem.Tests
{
    [TestFixture]
    public class InventoryTests
    {
        private Inventory inventory;
        private InventoryItem testItem;

        [SetUp]
        public void SetUp()
        {
            testItem = ItemFactory("Test Item", 10);
            inventory = new Inventory(5);
        }

        [TearDown]
        public void TearDown()
        {
            inventory = null;
            testItem = null;
        }

        private InventoryItem ItemFactory(string name = "Item", int maxAmount = 10)
        {
            InventoryItem _item = ScriptableObject.CreateInstance<InventoryItem>();
            _item.Name = name;
            _item.MaxAmount = maxAmount;
            return _item;
        }

        [Test]
        public void Constructor_WithNegativeInitialSlotCount_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Inventory(-1));
        }

        [Test]
        public void Constructor_WithNullInitialSlots_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Inventory(null));
        }

        [Test]
        public void Constructor_WithItemAndAmount_AddsItemToInventory()
        {
            Inventory inventoryWithItem = new Inventory(testItem, 5, 5);
            Assert.AreEqual(5, inventoryWithItem.SearchItem(testItem));
        }

        [Test]
        public void AddItem_WithNullSourceInventory_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => inventory.AddItem(null));
        }

        [Test]
        public void AddItem_WithExistingItem_AddsToExistingSlot()
        {
            inventory.AddItem(testItem, 5);
            int remaining = inventory.AddItem(testItem, 3);
            Assert.AreEqual(0, remaining);
            Assert.AreEqual(8, inventory.SearchItem(testItem));
        }

        [Test]
        public void AddItem_WithNewItem_AddsToEmptySlot()
        {
            InventoryItem newItem = ItemFactory("New Item", 5);
            int remaining = inventory.AddItem(newItem, 3);
            Assert.AreEqual(0, remaining);
            Assert.AreEqual(3, inventory.SearchItem(newItem));
        }

        [Test]
        public void SubtractItem_WithNullItem_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => inventory.SubtractItem(null, 1));
        }

        [Test]
        public void SubtractItem_WithInsufficientAmount_ReturnsRemainingAmount()
        {
            inventory.AddItem(testItem, 5);
            int remaining = inventory.SubtractItem(testItem, 7);
            Assert.AreEqual(2, remaining);
            Assert.AreEqual(0, inventory.SearchItem(testItem));
        }

        [Test]
        public void SearchItem_WithNullItem_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => inventory.SearchItem(null));
        }

        [Test]
        public void ExchangeItems_WithNullSenderInventory_ThrowsArgumentNullException()
        {
            Inventory exchangeInventory = new Inventory(1);
            Assert.Throws<ArgumentNullException>(() => inventory.ExchangeItems(null, exchangeInventory));
        }

        [Test]
        public void ExchangeItems_WithNullExchangedItems_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => inventory.ExchangeItems(inventory, null));
        }

        [Test]
        public void ToString_ReturnsCorrectFormat()
        {
            inventory.AddItem(testItem, 5);
            string result = inventory.ToString();
            Assert.IsTrue(result.Contains("Test Item"));
            Assert.IsTrue(result.Contains("Current Amount: 5"));
        }

        [Test]
        public void ShrinkSlots_WithNegativeRemovedSlots_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => inventory.ShrinkSlots(-1));
        }

        [Test]
        public void ShrinkSlots_WithTooManyRemovedSlots_ThrowsException()
        {
            Assert.Throws<Exception>(() => inventory.ShrinkSlots(6));
        }

        [Test]
        public void ExpandSlots_WithNegativeAdditionalSlots_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => inventory.ExpandSlots(-1));
        }

        [Test]
        public void AddItem_WithZeroAmount_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => inventory.AddItem(testItem, 0));
        }

        [Test]
        public void SubtractItem_WithZeroAmount_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => inventory.SubtractItem(testItem, 0));
        }
    }
}