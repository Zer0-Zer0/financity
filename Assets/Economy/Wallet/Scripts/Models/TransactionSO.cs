using UnityEngine;

namespace Economy
{
    [CreateAssetMenu(fileName = "TransactionSO", menuName = "TransactionSO", order = 0)]
    public class TransactionSO : ScriptableObject
    {
        public Transaction instance;
    }
}