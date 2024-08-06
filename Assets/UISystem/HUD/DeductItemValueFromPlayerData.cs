using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;

public class DeductItemValueFromPlayerData : MonoBehaviour
{
    [SerializeField] private PlayerDataEvents playerDataEvents;
    public void DeductValueFromPlayerData(Component sender, object data)
    {
        if (data is InventorySlot slot)
        {
            playerDataEvents.AddToCurrentBalance(slot.CurrentItem.GetCurrentValue() * -1 );
        }
    }
}
