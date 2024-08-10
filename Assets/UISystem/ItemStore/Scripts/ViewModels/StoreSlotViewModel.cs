using System;
using UnityEngine;
using UISystem;
using Inventory;

public class StoreSlotViewModel : MonoBehaviour
{
    [SerializeField]
    private StoreSlotView storeSlotView;

    [SerializeField]
    private InventoryItem item;

    private void OnEnable() => storeSlotView.UpdateSlotGraphics(item);

    public void UpdateSlotGraphics(Component sender, object data) =>
        storeSlotView.UpdateSlotGraphics(item);
}

