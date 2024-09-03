using System;
using UnityEngine;

namespace Economy
{
    [CreateAssetMenu(fileName = "TransactionSO", menuName = "Economy/TransactionSO", order = 0)]
    public class TransactionSO : ScriptableObject
    {
        [SerializeField] string nome;
        [SerializeField] float valor;

        [SerializeField] TipoDeTransação tipoDeTransação;

        public Transaction Instance
        {
            get
            {
                if (tipoDeTransação == TipoDeTransação.Adição)
                    return new Transaction(valor, DataManager.playerData.GetCurrentWallet(), null, nome);
                else if (tipoDeTransação == TipoDeTransação.Remoção)
                    return new Transaction(valor, null, DataManager.playerData.GetCurrentWallet(), nome);
                else
                    throw new NotImplementedException("ERROR: uninplemented transaction type");
            }
        }

        enum TipoDeTransação
        {
            Adição,
            Remoção
        }
    }
}