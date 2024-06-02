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
    public readonly float Total;
    public readonly float Principal;
    public readonly float Rate;
    public readonly int Installments;
    public readonly LoanData.Type LoanType;

    public static UnityEvent<LoanData> OnLoanPaymentComplete;
    public static UnityEvent<LoanData> OnInstallmentPayment;

    private float _remainingValue;
    public float RemainingValue
    {
        get
        {
            return _remainingValue;
        }
        set
        {
            try
            {
                if (value > 0)
                {
                    throw new Exception("Remaining loan value got netagive");
                }
                else if (value != 0)
                {
                    OnInstallmentPayment.Invoke(this);
                }
                _remainingInstallments = value;
            }
            catch (Exception ex)
            {
                Debug.LogError("Error updating remaining loan value; " + ex.Message);
            }
        }
    }

    private float _remainingInstallments;
    public float RemainingInstallments
    {
        get
        {
            return _remainingInstallments;
        }
        set
        {
            try
            {
                if (value > 0)
                {
                    throw new Exception("Remaining Installments got netagive");
                }
                else if (value == 0)
                {
                    OnLoanPaymentComplete.Invoke(this);
                }
                _remainingInstallments = value;
            }
            catch (Exception ex)
            {
                Debug.LogError("Error updating remaining installments; " + ex.Message);
            }
        }
    }

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

    public LoanData(float principal, float rate, int installments, LoanData.Type type, float total = 0)
    {
        Total = total;
        if (total == 0)
        {
            switch (type)
            {
                case LoanData.Type.SimpleInterest:
                    Total = CalculateSimpleInterest(principal, rate);
                    break;
                case LoanData.Type.CompoundInterest:
                    Total = CalculateCompoundInterest(principal, rate, installments);
                    break;
            }
        }
        _remainingValue = Total;
        Principal = principal;
        Rate = rate;
        Installments = installments;
        LoanType = type;
        _remainingInstallments = installments;
    }

    /// <summary>
    /// Calculates the installment amount for the loan.
    /// </summary>
    /// <returns>The installment amount for the loan.</returns>
    internal float GetInstallmentValue()
    {
        return Total / Installments;
    }

    /// <summary>
    /// Calculates the compound interest based on the principal, rate, and installments.
    /// </summary>
    /// <param name="principal">The loaned amount of money, without the interest.</param>
    /// <param name="rate">The interest rate per period, in percent.</param>
    /// <param name="installments">The number of periods the interest is applied.</param>
    /// <returns>A LoanData struct with the calculated values after applying compound interest.</returns>
    public static float CalculateCompoundInterest(float principal, float rate, int installments)
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
    public static float CalculateSimpleInterest(float principal, float rate)
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
            $"Loan Type: {LoanType}\n" +
            $"Remaining Value: {_remainingValue}\n" +
            $"Remaining Installments: {_remainingInstallments}";
    }
}