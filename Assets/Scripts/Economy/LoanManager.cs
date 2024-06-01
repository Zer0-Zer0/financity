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
    [System.Serializeable]
    public struct LoanData{
        internal readonly float TotalValue;
        internal readonly float PrincipalValue;
        internal readonly float RateValue;
        internal readonly int TimeValue;
        internal readonly LoanType loanType;

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
        rate += 1;
        float _compoundInterest = Mathf.Pow(rate, time);
        float _totalValue = principal * _compoundInterest;
        return new LoanData(_totalValue, principal, rate, time);
    }

    /// <summary>
    /// Calculates the simple interest based on the principal and rate.
    /// </summary>
    /// <param name="principal">The loaned amount of money, without the interest.</param>
    /// <param name="rate">The interest rate per period, in percent.</param>
    /// <param name="time">The number of periods the interest is applied.</param>
    /// <returns>A LoanData struct with the calculated values after applying simple interest.</returns>
    public static LoanData CalculateSimpleInterest(float principal, float rate, int time){
        rate += 1;
        float _totalValue = principal * rate;
        return new LoanData(_totalValue, principal, rate, time);
    }

    /// <summary>
    /// Makes a loan for the player.
    /// </summary>
    /// <param name="Wallet">The wallet manager of the player.</param>
    /// <param name="loanData">The data of the loan to be made.</param>
    /// <param name="loanType">The type of loan to be made.</param>
    /// <remarks>
    /// If the new debt exceeds the current maximum debt, an error is logged.
    /// </remarks>
    public static void MakeALoan(WalletManager Wallet, LoanData loanData, LoanType loanType){
        float newDebt = Wallet.CurrentDebt + loanData.TotalValue;
        if(newDebt > Wallet.CurrentMaxDebt){
            Debug.LogError("ERRO! Tentativa de fazer empréstimo quando débito máximo é menor que o débito atual, adicione mais condicionais");
        }else{
            Wallet.CurrentDebt = newDebt;
            Wallet.CurrentDigitalMoney += LoanData.PrincipalValue; 
        }
    }
}