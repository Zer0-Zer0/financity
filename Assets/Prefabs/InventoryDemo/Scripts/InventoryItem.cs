using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string Name;
    public int MaxAmount;
    public string Description;
    public Sprite Icon;

    public override string ToString()
    {
        return $"Item: {Name}, Max Amount: {MaxAmount}";
    }
}