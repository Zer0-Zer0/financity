using UnityEngine;

[System.Serializable]
public class Slot
{
    public Item item;
    public int amount;
}

public class Inventory : MonoBehaviour
{
    public Slot[] slots;

    public void AddItem(Item item, int amount)
    {
        foreach (Slot slot in slots)
        {
            if (slot.item == item)
            {
                slot.amount += amount;
                return;
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].item = item;
                slots[i].amount = amount;
                return;
            }
        }
    }

    public void RemoveItem(Item item, int amount)
    {
        foreach (Slot slot in slots)
        {
            if (slot.item == item)
            {
                slot.amount -= amount;
                if (slot.amount <= 0)
                {
                    slot.item = null;
                    slot.amount = 0;
                }
                return;
            }
        }
    }
}
