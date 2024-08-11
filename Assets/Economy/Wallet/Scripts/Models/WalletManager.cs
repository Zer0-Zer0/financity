using System;
using System.Collections;
using System.Collections.Generic;
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

        public void OnLoanRecieved(Component sender, object data)
        {
            if (data is LoanProcessor loanProcessor)
                loanProcessor.GrantLoan(Wallet);
        }

        public void PayInstallments(Component sender, object data)
        {
            if (Wallet == null || Wallet.Loans == null)
                return;

            List<LoanProcessor> loansToRemove = new List<LoanProcessor>();

            foreach (var loan in Wallet.Loans)
            {
                loan.OnInstallmentArrival(Wallet);
                if (loan.TotalRemainingInstallments == 0)
                    loansToRemove.Add(loan);
            }

            foreach (var loan in loansToRemove)
                loan.Cleanup(Wallet);
        }
    }
}
