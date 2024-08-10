using System;
using UnityEngine;

namespace Economy
{
    public enum TransactionPosition
    {
        Receiver,
        Sender
    }

    /// <summary>
    /// Manages the wallet data and updates UI elements accordingly.
    /// </summary>
    public class WalletManager : MonoBehaviour
    {
        /// <summary>
        /// Reference to the WalletData scriptable object.
        /// </summary>
        public WalletData Wallet;

        public Transaction MakeTransaction(float value, WalletData receiver, TransactionType type)
        {
            Transaction transactionToMake = new Transaction(value, type, Wallet, receiver);
            TransactionValidation(transactionToMake);
            transactionToMake.OnTransactionAccepted.AddListener(OnTransactionAcceptedEventHandler);
            return transactionToMake;
        }

        public void ReceiveTransaction(Transaction transaction)
        {
            TransactionValidation(transaction);
            transaction.OnTransactionAccepted?.Invoke(transaction);
        }

        private TransactionPosition VerifyTransactionPosition(Transaction transaction)
        {
            if (transaction.Sender == Wallet)
                return TransactionPosition.Sender;
            else if (transaction.Receiver == Wallet)
                return TransactionPosition.Receiver;
            else
                throw new InvalidOperationException("Transaction verification failed: Wallet is neither sender nor receiver.");
        }

        private void TransactionValidation(Transaction transaction)
        {
            VerifySenderMoney(transaction);
            VerifyTransactionPosition(transaction);
        }

        private static void VerifySenderMoney(Transaction transaction)
        {
            float senderBalance = transaction.Type == TransactionType.Physical
                ? transaction.Sender.CurrentPhysicalMoney
                : transaction.Sender.CurrentDigitalMoney;

            if (senderBalance < transaction.Value)
            {
                throw new InvalidOperationException($"Transaction validation failed: Insufficient funds in sender's wallet for {transaction.Type} transaction.");
            }
        }

        private void OnTransactionAcceptedEventHandler(Transaction transaction)
        {
            UpdateWallets(transaction);
            transaction.OnTransactionAccepted?.RemoveListener(OnTransactionAcceptedEventHandler);
        }

        private void UpdateWallets(Transaction transaction)
        {
            transaction.Receiver.Transactions.Add(transaction);
            transaction.Sender.Transactions.Add(transaction);
        }

        public void OnLoanRecieved(Component sender, object data){
            if(data is LoanProcessor loanProcessor){
                Wallet.Loans.Add(loanProcessor);
            }
        }
    }
}