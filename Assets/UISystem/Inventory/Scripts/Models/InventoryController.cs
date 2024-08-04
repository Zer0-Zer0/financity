using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UISystem
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private int _initialSlotCount;

        [Header("Event")]
        [SerializeField] private GameEvent OnInventoryChanged;

        private Inventory _inventory;
        private void Awake()
        {
            _inventory = new Inventory(_initialSlotCount);
        }

        private void Start()
        {
            OnInventoryChanged.Raise(this, _inventory.slots);
        }

        public void OnInventoryItemSubtracted(Component sender, object data)
        {
            if (data is Inventory items)
            {
                _inventory.SubtractItem(items);
                OnInventoryChanged.Raise(this, _inventory.slots);
            }
        }


    }
}