using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using Economy;

[RequireComponent(typeof(PlayerDataEvents))]
public class DeductItemValueFromPlayerData : MonoBehaviour
{
    private PlayerDataEvents playerDataEvents;

    private void OnEnable() => playerDataEvents = GetComponent<PlayerDataEvents>();

    public void DeductValueFromPlayerData(Component sender, object data)
    {
        if (data is InventorySlot slot)
        {
            Transaction transaction = new Transaction(slot.CurrentItem.GetCurrentValue(), null, DataManager.playerData.GetCurrentWallet(), slot.CurrentItem.Name);
            playerDataEvents.AddTransaction(transaction);
        }
    }
}
