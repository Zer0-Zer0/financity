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

        private void Start() => storeSlotView.UpdateSlotGraphics(item);

        private void OnEnable() => storeSlotView.UpdateSlotGraphics(item);

        public void UpdateSlotGraphics(Component sender, object data) =>
            storeSlotView.UpdateSlotGraphics(item);
    }
}
