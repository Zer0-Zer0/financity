using Economy;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public string Description;
    public int MaxAmount;

    [Header("Scriptable Objects")]
    [SerializeField]
    private DynamicSecuritySO securitySO;

    [SerializeField]
    public GameEvent OnItemConsumeEvent;

    public float GetCurrentValue()
    {
        return securitySO.currentValue;
    }

    public override string ToString()
    {
        return $"Item: {Name}, Max Amount: {MaxAmount}";
    }
}
