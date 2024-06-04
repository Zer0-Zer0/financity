using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages loans and their associated data, including installments, penalties, and repayment.
/// This class provides events for loan-related actions, such as loan granting, installment payment, and loan repayment.
/// It also generates new random loans when the current loan is fully repaid.
/// </summary>
public class LoanManager : MonoBehaviour
{
    public UnityEvent<LoanData> LoanGranted;
    public UnityEvent<LoanData> LoanFullyRepaid;
    public UnityEvent<LoanData> InstallmentPaid;
    public UnityEvent<LoanData> InstallmentArrived;
    public UnityEvent<LoanData> InstallmentLate;
    public UnityEvent PersistenceChanged;

    LoanData _loan;

    float _remainingValue;
    float _remainingInstallments;
    float _installmentValue
    {
        get
        {
            return _remainingValue / _remainingInstallments;
        }
    }

    int _remainingPenaltyInstallments;
    float _rawRemainingPenalty;

    float _calculatedRemainingPenalty
    {
        get
        {
            return LoanData.CalculateTotalFromCompoundInterest(_rawRemainingPenalty, _loan.Rate, _remainingPenaltyInstallments);
        }
    }
    float _remainingPenaltyInstallmentsValue
    {
        get
        {
            return _calculatedRemainingPenalty / _remainingPenaltyInstallments;
        }
    }

    float _totalToPay
    {
        get
        {
            return _calculatedRemainingPenalty + _remainingValue;
        }
    }

    bool _isPersistent;

    public void SetLoanData(LoanData Loan)
    {
        _loan = Loan;
    }

    /// <summary>
    /// Called when an installment arrives.
    /// </summary>
    /// <param name="wallet">The player's wallet data.</param>
    public void OnInstallmentArrival(WalletData wallet)
    {
        if (wallet.CurrentDigitalMoney >= _installmentValue)
        {
            OnInstallmentPaid(wallet);
        }
        else
        {
            OnInstallmentLate(wallet);
        }
        InstallmentArrived?.Invoke(_loan);
    }

    /// <summary>
    /// Called when an installment is paid.
    /// </summary>
    /// <param name="wallet">The player's wallet data.</param>
    public void OnInstallmentPaid(WalletData wallet)
    {
        if (_remainingPenaltyInstallments != 0)
        {
            wallet.CurrentDigitalMoney -= _remainingPenaltyInstallmentsValue;
            _rawRemainingPenalty -= _remainingPenaltyInstallmentsValue;
            _remainingPenaltyInstallments--;
        }
        else
        {
            wallet.CurrentDigitalMoney -= _installmentValue;
            _remainingValue -= _installmentValue;
            _remainingInstallments--;
        }
        InstallmentPaid?.Invoke(_loan);
    }

    /// <summary>
    /// Called when an installment is late.
    /// </summary>
    /// <param name="wallet">The player's wallet data.</param>
    public void OnInstallmentLate(WalletData wallet)
    {
        if (_remainingInstallments != 0)
        {
            _rawRemainingPenalty += _installmentValue;
            _remainingPenaltyInstallments++;

            _remainingValue -= _installmentValue;
            _remainingInstallments--;
        }
        else
        {
            _remainingPenaltyInstallments++;
        }
        InstallmentLate?.Invoke(_loan);
    }

    /// <summary>
    /// Called when a loan is fully repaid by the player.
    /// </summary>
    /// <param name="wallet">The player's wallet data.</param>
    public void OnLoanFullyRepaid(WalletData wallet)
    {
        wallet.CurrentDigitalMoney -= _totalToPay;
        resetLoan();

        PersistenceChanged?.Invoke();
        LoanFullyRepaid?.Invoke(_loan);
    }

    /// <summary>
    /// Resets the loan to its initial state and generates a new local random loan.
    /// </summary>
    void resetLoan()
    {
        _remainingValue = 0;
        _remainingInstallments = 0;
        _remainingPenaltyInstallments = 0;
        _rawRemainingPenalty = 0;
        _isPersistent = false;
        NewLocalRandomLoan();
    }

    /// <summary>
    /// Called when a loan is granted to the player.
    /// </summary>
    /// <param name="wallet">The player's wallet data.</param>
    public void OnLoanGranted(WalletData wallet)
    {
        wallet.CurrentDigitalMoney += _loan.Principal;
        wallet.CurrentDebt += _loan.Total;

        _remainingValue = _loan.Total;
        _remainingInstallments = _loan.Installments;
        _isPersistent = true;
        PersistenceChanged?.Invoke();
        LoanGranted?.Invoke(_loan);
    }

    /// <summary>
    /// Generates a new local random loan if the current loan is not persistent.
    /// </summary>
    public void NewLocalRandomLoan()
    {
        if (!_isPersistent)
        {
            LoanData _newLoan = RandomLoan(_loan.LoanType);
            SetLoanData(_newLoan);
        }
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
    public static LoanData RandomLoan(LoanData.Type type, float minPrincipal = 300, float maxPrincipal = 800, float minRate = 0.05f, float maxRate = 0.15f, int minInstallments = 1, int maxInstallments = 3)
    {
        float _principal = UnityEngine.Random.Range(minPrincipal, maxPrincipal);
        float _rate = UnityEngine.Random.Range(minRate, maxRate);
        int _installments = UnityEngine.Random.Range(minInstallments, maxInstallments + 1);

        return new LoanData(_principal, _rate, _installments, type);
    }
}