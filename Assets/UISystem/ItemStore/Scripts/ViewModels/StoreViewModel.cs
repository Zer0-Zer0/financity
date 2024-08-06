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

        public void OnEconomyTick(Component sender, object data)
        {
            /*TODO: MAKE THIS USEFULL
            if (data is List<InventorySlot> slots)
                _storeView.UpdateSlotsGraphics(slots);*/
        }
    }
}
