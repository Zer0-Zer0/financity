using System.Collections;
using System.Collections.Generic;
using System;
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
        [SerializeField] private LoanData loanData;
        public LoanData Loan
        {
            get => loanData;
            set => loanData = value;
        }

        private float remainingValue;
        private float remainingInstallments;
        private float InstallmentValue => remainingValue / remainingInstallments;

        private int remainingPenaltyInstallments;
        private float rawRemainingPenalty;

        private float RemainingPenalty => LoanData.CalculateTotalFromCompoundInterest(rawRemainingPenalty, Loan.Rate, remainingPenaltyInstallments);
        private float RemainingPenaltyInstallmentValue => CalculateRemainingPenaltyInstallmentValue();
        public float TotalToPay => CalculateTotalToPay();

        public LoanProcessor(LoanData loanData)
        {
            Loan = loanData;
        }

        private float CalculateTotalToPay() => RemainingPenalty + remainingValue;
        private float CalculateRemainingPenaltyInstallmentValue() => RemainingPenalty / remainingPenaltyInstallments;

        public void SetLoanData(LoanData loan) => Loan = loan;

        /// <summary>
        /// Called when an installment arrives.
        /// </summary>
        /// <param name="wallet">The player's wallet data.</param>
        public void OnInstallmentArrival(WalletData wallet)
        {
            if (wallet.CurrentDigitalMoney >= InstallmentValue)
                ProcessInstallmentPayment(wallet);
            else
                ProcessLateInstallmentPayment(wallet);
        }

        /// <summary>
        /// Called when an installment is paid.
        /// </summary>
        /// <param name="wallet">The player's wallet data.</param>
        private void ProcessInstallmentPayment(WalletData wallet)
        {
            Transaction transaction;
            if (remainingPenaltyInstallments != 0)
            {
                transaction = new Transaction(RemainingPenaltyInstallmentValue, TransactionType.Digital, null, wallet);
                rawRemainingPenalty -= RemainingPenaltyInstallmentValue;
                remainingPenaltyInstallments--;
            }
            else
            {
                transaction = new Transaction(InstallmentValue, TransactionType.Digital, null, wallet);
                remainingValue -= InstallmentValue;
                remainingInstallments--;
            }
            wallet.Transactions.Add(transaction);
        }

        /// <summary>
        /// Called when an installment is late.
        /// </summary>
        /// <param name="wallet">The player's wallet data.</param>
        private void ProcessLateInstallmentPayment(WalletData wallet)
        {
            if (remainingPenaltyInstallments != 0)
            {
                rawRemainingPenalty -= wallet.CurrentDigitalMoney;
            }
            else
            {
                remainingValue -= wallet.CurrentDigitalMoney;
                remainingInstallments--;
            }

            Transaction transaction = new Transaction(wallet.CurrentDigitalMoney, TransactionType.Digital, null, wallet);
            wallet.Transactions.Add(transaction);

            if (remainingInstallments != 0)
            {
                // Moves the installment value from normal to the penalty one
                rawRemainingPenalty += InstallmentValue;
                remainingPenaltyInstallments++;

                remainingValue -= InstallmentValue;
                remainingInstallments--;
            }
            else
                remainingPenaltyInstallments++;
        }

        /// <summary>
        /// Called when a loan is fully repaid by the player.
        /// </summary>
        /// <param name="wallet">The player's wallet data.</param>
        private void ProcessLoanFullyRepaid(WalletData wallet)
        {
            Transaction transaction = new Transaction(TotalToPay, TransactionType.Digital, null, wallet);
            wallet.Transactions.Add(transaction);
            ResetLoanProcessor();
        }

        /// <summary>
        /// Called when a loan is granted to the player.
        /// </summary>
        /// <param name="wallet">The player's wallet data.</param>
        public void GrantLoan(WalletData wallet)
        {
            Transaction transaction = new Transaction(Loan.Principal, TransactionType.Digital, wallet);
            wallet.Transactions.Add(transaction);
            wallet.Loans.Add(this);

            remainingValue = Loan.Total;
            remainingInstallments = Loan.Installments;
        }

        /// <summary>
        /// Resets the loan to its initial state and generates a new local random loan.
        /// </summary>
        public void ResetLoanProcessor()
        {
            remainingValue = 0;
            remainingInstallments = 0;
            remainingPenaltyInstallments = 0;
            rawRemainingPenalty = 0;
            GenerateNewLocalRandomLoan();
        }

        /// <summary>
        /// Generates a new local random loan if the current loan is not persistent.
        /// </summary>
        public void GenerateNewLocalRandomLoan()
        {
            LoanType type = RandomEnum.GetRandomEnumValue<LoanType>();
            LoanData newLoan = GenerateRandomLoan(type);
            SetLoanData(newLoan);
        }

        /// <summary>
        /// Generates a random loan with specified parameters.
        /// </summary>
        /// <param name="minPrincipal">The minimum principal amount for the loan.</param>
        /// <param name="maxPrincipal">The maximum principal amount for the loan.</param>
        /// <param name="minRate">The minimum interest rate for the loan.</param>
        /// <param name="maxRate">The maximum interest rate for the loan.</param>
        /// <param name="minInstallments">The minimum number of installments for the loan.</param>
        /// <param name="maxInstallments">The maximum number of installments for the loan.</param>
        /// <param name="type">The type of the loan.</param>
        /// <returns>A new LoanData object with randomly generated principal, rate, installments, and type.</returns>
        public static LoanData GenerateRandomLoan(LoanType type, float minPrincipal = 1000, float maxPrincipal = 1500, float minRate = 0.10f, float maxRate = 0.30f, int minInstallments = 4, int maxInstallments = 7)
        {
            float principal = UnityEngine.Random.Range(minPrincipal, maxPrincipal);
            float rate = UnityEngine.Random.Range(minRate, maxRate);
            int installments = UnityEngine.Random.Range(minInstallments, maxInstallments + 1);

            return new LoanData(principal, rate, installments, type);
        }
    }
}
