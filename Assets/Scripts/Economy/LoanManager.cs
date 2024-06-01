using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoanManager : MonoBehaviour
{
    /// <summary>
    /// Represents data for a loan, including total value, principal value, interest rate, installment value, and time value.
    /// </summary>
    /// <param name="TotalValue">The total value of the loan.</param>
    /// <param name="PrincipalValue">The principal value of the loan.</param>
    /// <param name="RateValue">The interest rate of the loan, in percent.</param>
    /// <param name="TimeValue">The time duration of the loan.</param>
    [System.Serializeable]
    public struct LoanData{
        internal readonly float TotalValue;
        internal readonly float PrincipalValue;
        internal readonly float RateValue;
        internal readonly int TimeValue;
        internal readonly float GetInstallment(){
            return TotalValue/TimeValue;
        }
    }

    public static enum LoanType{
        SimpleInterest,
        CompountInterest
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

    public static void MakeALoan(WalletManager Wallet, LoanData loanData, LoanType loanType){
        switch (loanType){
            case LoanType.SimpleInterest:
            
            break;
            case LoanType.CompountInterest:
            break;
        }
    }
}