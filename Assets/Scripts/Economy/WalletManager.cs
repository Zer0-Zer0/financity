using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages digital and physical money, debt, and debt limits.
/// </summary>
public class WalletManager : MonoBehaviour
{
    [SerializeField] WalletData _wallet;

    /// <summary>
    /// Makes a loan for the player, by adding money and debt to it's account
    /// </summary>
    /// <param name="wallet">The wallet manager of the player.</param>
    /// <param name="loanData">The data of the loan to be made.</param>
    /// <remarks>
    /// If the new debt exceeds the current maximum debt, an error is logged.
    /// </remarks>
    void _onAcceptALoan(LoanData loanData)
    {
        try
        {
            float newDebt = _wallet.CurrentDebt + loanData.Total;

            if (newDebt > _wallet.CurrentMaxDebt)
            {
                throw new Exception("New debt exceeds the current maximum debt");
            }

            _wallet.CurrentDebt = newDebt;
            _wallet.CurrentDigitalMoney += loanData.Principal;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error making a loan: " + ex.Message);
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
    void _onPayAInstallment(LoanData loanData)
    {
        try
        {
            if (loanData.InstallmentValue > _wallet.CurrentDigitalMoney)
            {
                throw new Exception("Insufficient digital money to pay the installment");
            }

            _wallet.CurrentDebt -= loanData.InstallmentValue;
            loanData.RemainingValue -= loanData.InstallmentValue;
            loanData.RemainingInstallments--;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error paying a installment: " + ex.Message);
        }
    }

    void Awake()
    {
        LoanManager.OnAcceptALoan.AddListener(_onAcceptALoan);
        LoanManager.OnPayAInstallment.AddListener(_onPayAInstallment);
    }
}