using System;
using System.Collections.Generic;
using UnityEngine;

namespace Economy
{
    public class WalletManager : MonoBehaviour
    {
        public GameEvent OnLoanUnpaid;

        private void OnEnable() => DataManager.playerData.GetCurrentWallet().UnableToPay = OnLoanUnpaid;

        /// <summary>
        /// Handles the event triggered when a loan is received.
        /// Grants the loan to the current wallet using the provided LoanProcessor.
        /// </summary>
        /// <param name="sender">The component that triggered the loan received event.</param>
        /// <param name="data">The data associated with the event, expected to be an instance of LoanProcessor.</param>
        public void OnLoanReceived(Component sender, object data)
        {
            if (data is LoanProcessor loanProcessor)
                GrantLoanToWallet(loanProcessor);
        }

        /// <summary>
        /// Grants a loan to the current wallet by invoking the loan processor's grant method.
        /// This method retrieves the current wallet and applies the loan to it.
        /// </summary>
        /// <param name="loanProcessor">The loan processor responsible for handling the loan grant.</param>
        private void GrantLoanToWallet(LoanProcessor loanProcessor)
        {
            var currentWallet = DataManager.playerData.GetCurrentWallet();
            loanProcessor.GrantLoan(currentWallet);
        }

        /// <summary>
        /// Processes installment payments for all loans associated with the current wallet.
        /// If there are no loans, the method exits early without performing any actions.
        /// </summary>
        /// <param name="sender">The component that triggered the installment processing event.</param>
        /// <param name="data">The data associated with the event, not used in this method.</param>
        public void ProcessInstallments(Component sender, object data)
        {
            var currentWallet = DataManager.playerData.GetCurrentWallet();

            // Exit early if there are no loans to process
            if (
                currentWallet == null
                || currentWallet.Loans == null
                || currentWallet.Loans.Count == 0
            )
                return;

            List<LoanProcessor> loansToRemove = new List<LoanProcessor>();

            // Iterate through each loan and process its installment
            foreach (var loan in currentWallet.Loans)
                ProcessLoanInstallment(loan, currentWallet, loansToRemove);

            // Clean up any loans that have been fully paid off
            CleanupProcessedLoans(loansToRemove, currentWallet);
        }

        public void PassDay()
        {
            var currentWallet = DataManager.playerData.GetCurrentWallet();
            var value = currentWallet.CurrentMoney;
            currentWallet.Transactions.Clear();
            var transaction = new Transaction(value, currentWallet, null, "Saldo de ontem");
            currentWallet.Transactions.Add(transaction);
        }

        /// <summary>
        /// Processes the installment for a specific loan and checks if it has been fully paid off.
        /// If the loan is fully paid, it is marked for removal from the wallet.
        /// </summary>
        /// <param name="loan">The loan to process for installment payment.</param>
        /// <param name="currentWallet">The current wallet containing the loan.</param>
        /// <param name="loansToRemove">A list that will hold loans marked for removal after processing.</param>
        private void ProcessLoanInstallment(
            LoanProcessor loan,
            WalletData currentWallet,
            List<LoanProcessor> loansToRemove
        )
        {
            loan.OnInstallmentArrival(currentWallet);
            // Check if the loan has no remaining installments
            if (loan.TotalRemainingInstallments == 0)
                loansToRemove.Add(loan);
        }

        /// <summary>
        /// Cleans up loans that have been fully paid off by invoking their cleanup method.
        /// This method removes the loans from the current wallet and performs any necessary finalization.
        /// </summary>
        /// <param name="loansToRemove">The list of loans that have been fully paid and are ready for cleanup.</param>
        /// <param name="currentWallet">The current wallet containing the loans to be cleaned up.</param>
        private void CleanupProcessedLoans(
            List<LoanProcessor> loansToRemove,
            WalletData currentWallet
        )
        {
            foreach (var loan in loansToRemove)
                loan.Cleanup(currentWallet);
        }
    }
}
