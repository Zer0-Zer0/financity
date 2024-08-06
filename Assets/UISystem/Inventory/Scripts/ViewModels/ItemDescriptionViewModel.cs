using UnityEngine;

namespace UISystem
{
    public class ItemDescriptionViewModel : MonoBehaviour
    {
        [SerializeField]
        ItemDescriptionView itemDescriptionView;

        private void Start()
        {
            itemDescriptionView.UpdateItemDescription(new InventorySlot());
        }

        public void OnMarketTickUpdate(Component sender, object data)
        {
            if (data is InventorySlot slot)
                itemDescriptionView.UpdateItemValue(slot);
        }

        public void UpdateItemDescription(Component sender, object data)
        {
            if (data is InventorySlot slot)
                itemDescriptionView.UpdateItemDescription(slot);
        }
    }
}
