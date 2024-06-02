using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
public class LoanManager: MonoBehaviour
{
    public static UnityEvent<LoanData> OnAcceptALoan;
    public static UnityEvent<LoanData> OnPayAInstallment; 

    LoanData _loan;

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
    public static LoanData RandomLoan(LoanData.Type type, float minPrincipal = 300, float maxPrincipal = 800, float minRate = 0.05f, float maxRate = 0.15f, int minInstallments = 1, int maxInstallments = 3)
    {
        float _principal = UnityEngine.Random.Range(minPrincipal, maxPrincipal);
        float _rate = UnityEngine.Random.Range(minRate, maxRate);
        int _installments = UnityEngine.Random.Range(minInstallments, maxInstallments + 1);

        return new LoanData(_principal, _rate, _installments, type);
    }

    public void NewLocalRandomLoan(){
        _loan = RandomLoan(_loan.LoanType);
        Debug.Log(_loan.ToString());
    }

    public void OnLoanAccepted(){

    }
}