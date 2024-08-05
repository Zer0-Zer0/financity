using Economy;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public int MaxAmount;
    public string Description;
    [SerializeField ]private DynamicSecuritySO securitySO;

    public float GetCurrentValue(){
        return securitySO.currentValue;
    }

    public override string ToString()
    {
        return $"Item: {Name}, Max Amount: {MaxAmount}";
    }
}
