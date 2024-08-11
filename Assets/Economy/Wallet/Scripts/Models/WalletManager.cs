using System;
using System.Collections.Generic;
using UnityEngine;

namespace Economy
{
    /// <summary>
    /// Manages the wallet data and updates UI elements accordingly.
    /// </summary>
    public class WalletManager : MonoBehaviour
    {
        /// <summary>
        /// Reference to the WalletData scriptable object.
        /// </summary>
        public WalletData Wallet => DataManager.playerData.GetCurrentWallet();

        /// <summary>
        /// Handles the event when a loan is received.
        /// </summary>
        public void OnLoanReceived(Component sender, object data)
        {
            if (data is LoanProcessor loanProcessor)
                loanProcessor.GrantLoan(Wallet);
        }

        /// <summary>
        /// Processes the installment payments for all loans in the wallet.
        /// </summary>
        public void ProcessInstallments(Component sender, object data)
        {
            if (Wallet?.Loans == null || Wallet.Loans.Count == 0)
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
