using UnityEngine;

namespace UISystem
{
    public class InventorySlotViewModel : MonoBehaviour
    {
        [SerializeField] private InventorySlotView inventorySlotView;

        public void UpdateSlotGraphics(InventorySlot slot)
        {
            inventorySlotView.UpdateSlotGraphics(slot);
        }
    }
}