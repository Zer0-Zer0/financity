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
        /// Handles the event when a loan is received.
        /// </summary>
        public void OnLoanReceived(Component sender, object data)
        {
            if (data is LoanProcessor loanProcessor)
                loanProcessor.GrantLoan(DataManager.playerData.GetCurrentWallet());
        }

        /// <summary>
        /// Processes the installment payments for all loans in the wallet.
        /// </summary>
        public void ProcessInstallments(Component sender, object data)
        {
            if (
                DataManager.playerData.GetCurrentWallet()?.Loans == null
                || DataManager.playerData.GetCurrentWallet().Loans.Count == 0
            )
                return;

            List<LoanProcessor> loansToRemove = new List<LoanProcessor>();

            foreach (var loan in DataManager.playerData.GetCurrentWallet().Loans)
            {
                loan.OnInstallmentArrival(DataManager.playerData.GetCurrentWallet());
                if (loan.TotalRemainingInstallments == 0)
                    loansToRemove.Add(loan);
            }

            foreach (var loan in loansToRemove)
                loan.Cleanup(DataManager.playerData.GetCurrentWallet());
        }
    }
}
