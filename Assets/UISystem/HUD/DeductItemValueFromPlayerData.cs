using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

[RequireComponent(typeof(PlayerDataEvents))]
public class DeductItemValueFromPlayerData : MonoBehaviour
{
    private PlayerDataEvents playerDataEvents;

    private void OnEnable() => playerDataEvents = GetComponent<PlayerDataEvents>();

    public void DeductValueFromPlayerData(Component sender, object data)
    {
        if (data is InventorySlot slot)
        {
            playerDataEvents.RemoveFromCurrentBalance(slot.CurrentItem.GetCurrentValue());
        }
    }
}
