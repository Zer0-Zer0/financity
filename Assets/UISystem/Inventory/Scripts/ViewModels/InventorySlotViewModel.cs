using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystem
{
    public class InventorySlotViewModel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private InventorySlotView inventorySlotView;

        [SerializeField]
        private GameEvent OnMouseEnterGameEvent;

        private InventorySlot currentSlotData;

        public void UpdateSlotGraphics(InventorySlot slot)
        {
            currentSlotData = slot;
            inventorySlotView.UpdateSlotGraphics(currentSlotData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log("Mouse entered: " + currentSlotData.ToString()); // Demo stuff
            OnMouseEnterGameEvent.Raise(this, currentSlotData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log("Mouse exited: " + currentSlotData.ToString()); // Demo stuff
            OnMouseEnterGameEvent.Raise(this, new InventorySlot());
        }
    }
}
