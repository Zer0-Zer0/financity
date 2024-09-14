using System;
using UnityEngine;

namespace Economy
{
    public enum TipoDeTransação
    {
        Adição,
        Remoção
    }

    [CreateAssetMenu(fileName = "TransactionSO", menuName = "Economy/TransactionSO", order = 0)]
    public class TransactionSO : ScriptableObject
    {
        [SerializeField] string nome;
        [SerializeField] float valor;

        [SerializeField] TipoDeTransação tipoDeTransação;

        public Transaction Instance => new Transaction(valor, tipoDeTransação, nome);
    }
}