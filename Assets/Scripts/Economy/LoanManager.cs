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
    /// Represents data for a loan, including total value, principal value, interest rate, installment value, and time value.
    /// </summary>
    /// <param name="TotalValue">The total value of the loan.</param>
    /// <param name="PrincipalValue">The principal value of the loan.</param>
    /// <param name="RateValue">The interest rate of the loan, in percent.</param>
    /// <param name="TimeValue">The time duration of the loan.</param>
    /// <param name="loanType">The type of loan.</param>
    [System.Serializable]
    public struct LoanData{
        internal readonly float TotalValue;
        internal readonly float PrincipalValue;
        internal readonly float RateValue;
        internal readonly int TimeValue;
        internal readonly LoanType loanType;

        public LoanData(float totalValue, float principalValue, float rateValue, int timeValue, LoanType type)
        {
            TotalValue = totalValue;
            PrincipalValue = principalValue;
            RateValue = rateValue;
            TimeValue = timeValue;
            loanType = type;
        }

        /// <summary>
        /// Calculates the installment amount for the loan.
        /// </summary>
        /// <returns>The installment amount for the loan.</returns>
        internal readonly float GetInstallment(){
            return TotalValue/TimeValue;
        }
    }

    /// <summary>
    /// Calculates the compound interest based on the principal, rate, and time.
    /// </summary>
    /// <param name="principal">The loaned amount of money, without the interest.</param>
    /// <param name="rate">The interest rate per period, in percent.</param>
    /// <param name="time">The number of periods the interest is applied.</param>
    /// <returns>A LoanData struct with the calculated values after applying compound interest.</returns>
    public static LoanData CalculateCompoundInterest(float principal, float rate, int time){
        float _calculatedRate = rate + 1;
        float _compoundInterest = Mathf.Pow(_calculatedRate, time);
        float _totalValue = principal * _compoundInterest;
        return new LoanData(_totalValue, principal, rate, time, LoanType.CompoundInterest);
    }

    /// <summary>
    /// Calculates the simple interest based on the principal and rate.
    /// </summary>
    /// <param name="principal">The loaned amount of money, without the interest.</param>
    /// <param name="rate">The interest rate per period, in percent.</param>
    /// <param name="time">The number of periods the interest is applied.</param>
    /// <returns>A LoanData struct with the calculated values after applying simple interest.</returns>
    public static LoanData CalculateSimpleInterest(float principal, float rate, int time){
        float _calculatedRate = rate + 1;
        float _totalValue = principal * _calculatedRate;
        return new LoanData(_totalValue, principal, rate, time, LoanType.SimpleInterest);
    }

    /// <summary>
    /// Makes a loan for the player, by adding money and debt to it's account
    /// </summary>
    /// <param name="Wallet">The wallet manager of the player.</param>
    /// <param name="loanData">The data of the loan to be made.</param>
    /// <remarks>
    /// If the new debt exceeds the current maximum debt, an error is logged.
    /// </remarks>
    public static void MakeALoan(WalletManager Wallet, LoanData loanData){
        try
        {
        float newDebt = Wallet.CurrentDebt + loanData.TotalValue;

        if(newDebt > Wallet.CurrentMaxDebt){
            throw new Exception("New debt exceeds the current maximum debt");
        }

        Wallet.CurrentDebt = newDebt;
        Wallet.CurrentDigitalMoney += loanData.PrincipalValue; 
        }catch (Exception ex){
            Debug.LogError("Error making a loan: " + ex.Message)
        }
    }

    /// <summary>
    /// Pays a single installment of a loan from the wallet's digital money.
    /// </summary>
    /// <param name="Wallet">The wallet from which the installment will be paid.</param>
    /// <param name="loanData">The loan data containing information about the installment to be paid.</param>
    /// <remarks>
    /// This method checks if the current digital money in the wallet is sufficient to pay the installment of the loan.
    /// If the digital money is enough, it deducts the installment amount from the current debt in the wallet and updates the total loan value and remaining time.
    /// If the digital money is insufficient, an error message is logged.
    /// </remarks>
    /// <returns>A LoanData struct with the calculated values after paying the installment.</returns>
    public static LoanData PayAInstallment(WalletManager Wallet, LoanData loanData){
        try{
            if (loanData.GetInstallment() > Wallet.CurrentDigitalMoney){
                throw new Exception("Insufficient digital money to pay the installment");
            }
            float newDebt = Wallet.CurrentDebt - loanData.GetInstallment();
            float newTotal = loanData.TotalValue - loanData.GetInstallment();
            int newTime = loanData.TimeValue - 1;
            Wallet.CurrentDebt = newDebt;

            return new LoanData(newTotal, loanData.PrincipalValue, loanData.RateValue, newTime, loanData.LoanType);
            }catch (Exception ex){
            Debug.LogError("Error making a loan: " + ex.Message)
        }
    }
}