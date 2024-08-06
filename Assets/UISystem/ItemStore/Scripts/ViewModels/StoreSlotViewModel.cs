using UnityEngine;

namespace UISystem
{
    public class StoreSlotViewModel : MonoBehaviour
    {
        [SerializeField]
        private StoreSlotView storeSlotView;

        public void UpdateSlotGraphics(InventorySlot slot)
        {
            storeSlotView.UpdateSlotGraphics(slot);
        }
    }
}
