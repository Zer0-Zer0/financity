using System;
using UnityEngine;

namespace UISystem
{
    public class StoreSlotViewModel : MonoBehaviour
    {
        [SerializeField]
        private StoreSlotView storeSlotView;

        [SerializeField]
        private InventoryItem item;

        private void Awake()
        {
            storeSlotView.UpdateSlotGraphics(new InventorySlot(item, 1));
        }

        public void UpdateSlotGraphics(Component sender, object data)
        {
            storeSlotView.UpdateSlotGraphics(new InventorySlot(item, 1));
        }
    }
}
