using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoanManager : MonoBehaviour
{
    /// <summary>
    /// Represents the type of loan, which can be Simple Interest or Compound Interest.
    /// </summary>
    public enum LoanType
    {
        /// <summary>
        /// Represents a loan with simple interest calculation.
        /// </summary>
        SimpleInterest,

        /// <summary>
        /// Represents a loan with compound interest calculation.
        /// </summary>
        CompoundInterest
    }

    /// <summary>
    /// Represents data for a loan, including total value, principal value, interest rate, installment value, and installments value.
    /// </summary>
    /// <param name="Total">The total value of the loan.</param>
    /// <param name="Principal">The principal value of the loan.</param>
    /// <param name="Rate">The interest rate of the loan, in percent.</param>
    /// <param name="Installments">The installments duration of the loan.</param>
    /// <param name="loanType">The type of loan.</param>
    [System.Serializable]
    public struct LoanData{
        internal readonly float Total;
        internal readonly float Principal;
        internal readonly float Rate;
        internal readonly int Installments;
        internal readonly LoanType loanType;

        public LoanData(float total, float principal, float rate, int installments, LoanType type)
        {
            Total = total;
            Principal = principal;
            Rate = rate;
            Installments = installments;
            loanType = type;
        }

        /// <summary>
        /// Calculates the installment amount for the loan.
        /// </summary>
        /// <returns>The installment amount for the loan.</returns>
        internal float GetInstallment(){
            return Total/Installments;
        }
    }

    /// <summary>
    /// Calculates the compound interest based on the principal, rate, and installments.
    /// </summary>
    /// <param name="principal">The loaned amount of money, without the interest.</param>
    /// <param name="rate">The interest rate per period, in percent.</param>
    /// <param name="installments">The number of periods the interest is applied.</param>
    /// <returns>A LoanData struct with the calculated values after applying compound interest.</returns>
    public static LoanData CalculateCompoundInterest(float principal, float rate, int installments){
        float _calculatedRate = rate + 1;
        float _compoundInterest = Mathf.Pow(_calculatedRate, installments);
        float _total = principal * _compoundInterest;
        return new LoanData(_total, principal, rate, installments, LoanType.CompoundInterest);
    }

    /// <summary>
    /// Calculates the simple interest based on the principal and rate.
    /// </summary>
    /// <param name="principal">The loaned amount of money, without the interest.</param>
    /// <param name="rate">The interest rate per period, in percent.</param>
    /// <param name="installments">The number of periods the interest is applied.</param>
    /// <returns>A LoanData struct with the calculated values after applying simple interest.</returns>
    public static LoanData CalculateSimpleInterest(float principal, float rate, int installments){
        float _calculatedRate = rate + 1;
        float _total = principal * _calculatedRate;
        return new LoanData(_total, principal, rate, installments, LoanType.SimpleInterest);
    }

    /// <summary>
    /// Makes a loan for the player, by adding money and debt to it's account
    /// </summary>
    /// <param name="wallet">The wallet manager of the player.</param>
    /// <param name="loanData">The data of the loan to be made.</param>
    /// <remarks>
    /// If the new debt exceeds the current maximum debt, an error is logged.
    /// </remarks>
    public static void MakeALoan(WalletManager wallet, LoanData loanData){
        try
        {
        float newDebt = wallet.CurrentDebt + loanData.Total;

        if(newDebt > wallet.CurrentMaxDebt){
            throw new ArgumentOutOfRangeException("New debt exceeds the current maximum debt");
        }

        wallet.CurrentDebt = newDebt;
        wallet.CurrentDigitalMoney += loanData.Principal; 
        }catch (ArgumentOutOfRangeException ex){
            Debug.LogError("Error making a loan: ", ex.Message);
        }
    }

    /// <summary>
    /// Pays a single installment of a loan from the wallet's digital money.
    /// </summary>
    /// <param name="wallet">The wallet from which the installment will be paid.</param>
    /// <param name="loanData">The loan data containing information about the installment to be paid.</param>
    /// <remarks>
    /// This method checks if the current digital money in the wallet is sufficient to pay the installment of the loan.
    /// If the digital money is enough, it deducts the installment amount from the current debt in the wallet and updates the total loan value and remaining installments.
    /// If the digital money is insufficient, an error message is logged.
    /// </remarks>
    /// <returns>A LoanData struct with the calculated values after paying the installment.</returns>
    public static LoanData PayAInstallment(WalletManager wallet, LoanData loanData){
        try{
            if (loanData.GetInstallment() > wallet.CurrentDigitalMoney){
                throw new InvalidOperationException("Insufficient digital money to pay the installment");
            }
            float newDebt = wallet.CurrentDebt - loanData.GetInstallment();
            float newTotal = loanData.Total - loanData.GetInstallment();
            int newInstallments = loanData.Installments - 1;
            wallet.CurrentDebt = newDebt;

            return new LoanData(newTotal, loanData.Principal, loanData.Rate, newInstallments, loanData.loanType);
            }catch (InvalidOperationException ex){
            Debug.LogError("Error paying a installment: ", ex.Message);
        }

        return loanData;
    }
}