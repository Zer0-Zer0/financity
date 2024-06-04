using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents data for a loan, including total value, principal value, interest rate, installment value, and installments value.
/// </summary>
/// <param name="Total">The total value of the loan.</param>
/// <param name="Principal">The principal value of the loan.</param>
/// <param name="Rate">The interest rate of the loan, in percent.</param>
/// <param name="Installments">The installments duration of the loan.</param>
/// <param name="LoanData.Type">The type of loan.</param>
[System.Serializable]
public struct LoanData
{
    public float Total;
    public float Principal;
    public float Rate;
    public int Installments;
    public LoanData.Type LoanType;

    public static UnityEvent<LoanData> OnLoanPaymentComplete;
    public static UnityEvent<LoanData> OnInstallmentPayment;

    public readonly float InstallmentValue{
    get {
        return Total / Installments;
    }}

    /// <summary>
    /// Represents the type of loan, which can be Simple Interest or Compound Interest.
    /// </summary>
    public enum Type
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

    public LoanData(float principal, float rate, int installments, LoanData.Type type)
    {
        switch (type)
        {
            case LoanData.Type.SimpleInterest:
                this.Total = CalculateTotalFromSimpleInterest(principal, rate);
                break;
            default:
                this.Total = CalculateTotalFromCompoundInterest(principal, rate, installments);
                break;
        }

        this.Principal = principal;
        this.Rate = rate;
        this.Installments = installments;
        this.LoanType = type;
    }

    /// <summary>
    /// Calculates the principal amount from a given total amount and simple interest rate.
    /// </summary>
    /// <param name="total">The total amount.</param>
    /// <param name="rate">The simple interest rate.</param>
    /// <returns>The principal amount.</returns>
    public static float CalculatePrincipalFromSimpleInterest(float total, float rate)
    {
        float _calculatedRate = rate + 1;
        float _principal = total / _calculatedRate;
        return _principal;
    }

    /// <summary>
    /// Calculates the principal amount from a given total amount, compound interest rate, and number of installments.
    /// </summary>
    /// <param name="total">The total amount.</param>
    /// <param name="rate">The compound interest rate.</param>
    /// <param name="installments">The number of i
    public static float CalculatePrincipalFromCompoundInterest(float total, float rate, int installments)
    {
        float _calculatedRate = rate + 1;
        float _compoundInterest = Mathf.Pow(_calculatedRate, installments);
        float _principal = total / _compoundInterest;
        return _principal;
    }

    /// <summary>
    /// Calculates the compound interest based on the principal, rate, and installments.
    /// </summary>
    /// <param name="principal">The loaned amount of money, without the interest.</param>
    /// <param name="rate">The interest rate per period, in percent.</param>
    /// <param name="installments">The number of periods the interest is applied.</param>
    /// <returns>A LoanData struct with the calculated values after applying compound interest.</returns>
    public static float CalculateTotalFromCompoundInterest(float principal, float rate, int installments)
    {
        float _calculatedRate = rate + 1;
        float _compoundInterest = Mathf.Pow(_calculatedRate, installments);
        float _total = principal * _compoundInterest;
        return _total;
    }

    /// <summary>
    /// Calculates the simple interest based on the principal and rate.
    /// </summary>
    /// <param name="principal">The loaned amount of money, without the interest.</param>
    /// <param name="rate">The interest rate per period, in percent.</param>
    /// <returns>A LoanData struct with the calculated values after applying simple interest.</returns>
    public static float CalculateTotalFromSimpleInterest(float principal, float rate)
    {
        float _calculatedRate = rate + 1;
        float _total = principal * _calculatedRate;
        return _total;
    }

    public override string ToString()
    {
        return $"Loan Data:\n" +
            $"Total Value: {Total}\n" +
            $"Principal Value: {Principal}\n" +
            $"Interest Rate: {Rate * 100}%\n" +
            $"Installments: {Installments}\n" +
            $"Loan Type: {LoanType}\n";
    }
}