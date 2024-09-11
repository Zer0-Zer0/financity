using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Economy
{
    /// <summary>
    /// Manages loans and their associated data, including installments, penalties, and repayment.
    /// This class provides events for loan-related actions, such as loan granting, installment payment, and loan repayment.
    /// It also generates new random loans when the current loan is fully repaid.
    /// </summary>
    [Serializable]
    public class LoanProcessor
    {
        [SerializeField]
        private LoanData loan;

        /// <summary>
        /// Gets or sets the loan data associated with this loan processor.
        /// </summary>
        public LoanData Loan
        {
            get => loan;
            set => loan = value;
        }

        private float remainingValue;
        private int remainingInstallments;
        public float InstallmentValue
        {
            get
            {
                if (remainingInstallments != 0)
                    return remainingValue / remainingInstallments;
                else
                    return 0f;
            }
        }

        /// <summary>
        /// Gets the total number of remaining installments, including penalties.
        /// </summary>
        public int TotalRemainingInstallments => remainingInstallments;

        /// <summary>
        /// Calculates the total amount to be paid, including penalties and remaining loan value.
        /// </summary>
        public float TotalToPay => remainingValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoanProcessor"/> class with the specified loan data.
        /// </summary>
        /// <param name="loan">The loan data to initialize the processor with.</param>
        public LoanProcessor(LoanData loan)
        {
            Loan = loan;
        }

        /// <summary>
        /// Called when an installment payment is due.
        /// Processes the payment based on the player's wallet data.
        /// </summary>
        /// <param name="wallet">The player's wallet data.</param>
        public void OnInstallmentArrival(WalletData wallet)
        {
            if (wallet.CurrentMoney >= InstallmentValue)
                ProcessInstallmentPayment(wallet);
            else
                ProcessLateInstallmentPayment(wallet);
        }

        /// <summary>
        /// Processes a regular installment payment.
        /// Updates the wallet and loan state accordingly.
        /// </summary>
        /// <param name="wallet">The player's wallet data.</param>
        private void ProcessInstallmentPayment(WalletData wallet)
        {
            Transaction transaction;
            transaction = new Transaction(InstallmentValue, null, wallet, "Parcela");

            remainingValue -= InstallmentValue;
            remainingInstallments--;

            wallet.Transactions.Add(transaction);
        }

        /// <summary>
        /// Processes a late installment payment.
        /// Updates the wallet and loan state, potentially moving to penalty installments.
        /// </summary>
        /// <param name="wallet">The player's wallet data.</param>
        private void ProcessLateInstallmentPayment(WalletData wallet)
        {
            Debug.Log("Loose Condition or player looses the Item it gave for the loan");
            wallet.UnableToPay.Raise(null, Loan);
        }

        /// <summary>
        /// Processes the event when a loan is fully repaid by the player.
        /// Updates the wallet and resets the loan processor.
        /// </summary>
        /// <param name="wallet">The player's wallet data.</param>
        private void ProcessLoanFullyRepaid(WalletData wallet)
        {
            Transaction transaction = new Transaction(
                TotalToPay,
                null,
                wallet
            );
            wallet.Transactions.Add(transaction);
            ResetLoanProcessor();
        }

        /// <summary>
        /// Grants a loan to the player and updates their wallet.
        /// </summary>
        /// <param name="wallet">The player's wallet data.</param>
        public void GrantLoan(WalletData wallet)
        {
            Transaction transaction = new Transaction(
                Loan.Principal,
                wallet,
                null,
                "Emprestimo"
            );
            wallet.Transactions.Add(transaction);
            wallet.Loans.Add(this);

            remainingValue = Loan.Total;
            remainingInstallments = Loan.Installments;
        }

        /// <summary>
        /// Resets the loan processor to its initial state and generates a new local random loan.
        /// </summary>
        public void ResetLoanProcessor()
        {
            remainingValue = 0;
            remainingInstallments = 0;
            GenerateNewLocalRandomLoan();
        }

        /// <summary>
        /// Cleans up the loan processor by removing it from the wallet's loans and resetting its state.
        /// </summary>
        /// <param name="wallet">The player's wallet data.</param>
        public void Cleanup(WalletData wallet)
        {
            if (wallet.Loans.Contains(this))
                wallet.Loans.Remove(this);

            // Reset the loan data
            ResetLoanProcessor();
        }

        /// <summary>
        /// Generates a new local random loan if the current loan is not persistent.
        /// </summary>
        public void GenerateNewLocalRandomLoan()
        {
            LoanType type = RandomEnum.GetRandomEnumValue<LoanType>();
            LoanData newLoan = GenerateRandomLoan(type);
            Loan = newLoan;
        }

        /// <summary>
        /// Generates a random loan with specified parameters.
        /// </summary>
        /// <param name="type">The type of the loan.</param>
        /// <param name="minPrincipal">The minimum principal amount for the loan.</param>
        /// <param name="maxPrincipal">The maximum principal amount for the loan.</param>
        /// <param name="minRate">The minimum interest rate for the loan.</param>
        /// <param name="maxRate">The maximum interest rate for the loan.</param>
        /// <param name="minInstallments">The minimum number of installments for the loan.</param>
        /// <param name="maxInstallments">The maximum number of installments for the loan.</param>
        /// <returns>A new <see cref="LoanData"/> object with randomly generated principal, rate, installments, and type.</returns>
        public static LoanData GenerateRandomLoan(
            LoanType type,
            float minPrincipal = 1000,
            float maxPrincipal = 1500,
            float minRate = 0.10f,
            float maxRate = 0.30f,
            int minInstallments = 4,
            int maxInstallments = 7
        )
        {
            float principal = UnityEngine.Random.Range(minPrincipal, maxPrincipal);
            float rate = UnityEngine.Random.Range(minRate, maxRate);
            int installments = UnityEngine.Random.Range(minInstallments, maxInstallments + 1);

            return new LoanData(principal, rate, installments, type);
        }
    }
}
