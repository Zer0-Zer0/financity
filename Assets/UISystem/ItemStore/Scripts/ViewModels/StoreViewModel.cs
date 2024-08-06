using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public class StoreViewModel : MonoBehaviour
    {
        [SerializeField]
        private StoreView _storeView;

        public void OnInventoryChanged(Component sender, object data)
        {
            if (data is List<InventorySlot> slots)
                _storeView.UpdateSlotsGraphics(slots);
        }
    }
}
