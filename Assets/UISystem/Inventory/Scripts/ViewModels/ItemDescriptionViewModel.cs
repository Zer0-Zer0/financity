using UnityEngine;

namespace UISystem
{
    public class ItemDescriptionViewModel : MonoBehaviour
    {
        [SerializeField]
        ItemDescriptionView itemDescriptionView;

        public void OnMarketTickUpdate(Component sender, object data)
        {
            if (data is InventorySlot slot)
                itemDescriptionView.UpdateItemValue(slot);
        }

        public void OnMouseChangedSlot(Component sender, object data)
        {
            if (data is InventorySlot slot)
                itemDescriptionView.SetItemDescription(slot);
        }
    }
}
