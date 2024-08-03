using UnityEngine;

namespace UISystem
{
    public class InventorySlotViewModel : MonoBehaviour
    {
        [SerializeField] private InventorySlotView inventorySlotView;

        public void SetCurrentInventorySlot(InventorySlot slot)
        {
            inventorySlotView.UpdateSlotGraphics(slot);
        }
    }
}