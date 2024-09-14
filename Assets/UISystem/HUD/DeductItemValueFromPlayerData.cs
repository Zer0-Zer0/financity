using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using Economy;

public class DeductItemValueFromPlayerData : MonoBehaviour
{
    public void DeductValueFromPlayerData(Component sender, object data)
    {
        if (data is InventorySlot slot)
        {
            Transaction transaction = new Transaction(slot.CurrentItem.GetCurrentValue(), TipoDeTransação.Remoção, slot.CurrentItem.Name);
            DataManager.playerData.AddTransaction(transaction);
        }
    }
}
