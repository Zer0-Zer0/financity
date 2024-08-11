using Economy;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class InventoryItem : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
        public string Description;
        public int MaxAmount;

        [Header("Value Once")]
        public float data; //TODO: MAKE THIS SOME ABSTRACT VALUE

        [Header("Scriptable Objects")]
        [SerializeField]
        private DynamicSecuritySO securitySO;

        [SerializeField]
        public GameEvent OnItemConsumeEvent;

        public float GetCurrentValue() => securitySO.currentValue;

        public override string ToString() => $"Item: {Name}, Max Amount: {MaxAmount}";
    }
}