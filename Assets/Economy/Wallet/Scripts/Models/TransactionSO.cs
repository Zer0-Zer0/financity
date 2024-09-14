using System;
using UnityEngine;

namespace Economy
{
    public enum TipoDeTransação
    {
        Adição,
        Remoção
    }

    [CreateAssetMenu(fileName = "Transaction_", menuName = "ScriptableObjects/Economy/Transaction")]
    public class TransactionSO : ScriptableObject
    {
        [SerializeField] string nome;
        [SerializeField] float valor;

        [SerializeField] TipoDeTransação tipoDeTransação;

        public Transaction Instance => new Transaction(valor, tipoDeTransação, nome);
    }
}