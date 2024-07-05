using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string Name;
    public int MaxAmount;

    public override string ToString()
    {
        return $"Item: {Name}, Max Amount: {MaxAmount}";
    }
}