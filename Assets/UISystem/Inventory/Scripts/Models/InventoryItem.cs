using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public int MaxAmount;
    public string Description;

    public override string ToString()
    {
        return $"Item: {Name}, Max Amount: {MaxAmount}";
    }
}