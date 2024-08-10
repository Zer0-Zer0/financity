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
[Serializable]
public class LoanProcessor
{
    public UnityEvent<LoanData> LoanGrantOccurred;
    public UnityEvent<LoanData> LoanFullyRepaidEvent;
    public UnityEvent<LoanData> InstallmentPaymentMade;
    public UnityEvent<LoanData> InstallmentArrivalOccurred;
    public UnityEvent<LoanData> InstallmentPaymentLate;
    public UnityEvent PersistenceChanged;

    [SerializeField] private LoanData _loan;
    public LoanData Loan
    {
        get
        {
            return _loan;
        }
        set
        {
            _loan = value;
        }
    }

    float _remainingValue;
    float _remainingInstallments;
    float InstallmentValue
    {
        get
        {
            return _remainingValue / _remainingInstallments;
        }
    }

    int _remainingPenaltyInstallments;
    float _rawRemainingPenalty;

    float CalculatedRemainingPenalty
    {
        get
        {
            return LoanData.CalculateTotalFromCompoundInterest(_rawRemainingPenalty, Loan.Rate, _remainingPenaltyInstallments);
        }
    }
    float RemainingPenaltyInstallmentsValue
    {
        get
        {
            return CalculatedRemainingPenalty / _remainingPenaltyInstallments; 
        }
    }

    float TotalToPay
    {
        get
        {
            return CalculatedRemainingPenalty + _remainingValue;
        }
    }

    bool _isPersistent;

    public void SetLoanData(LoanData loan)
    {
        Loan = loan;
    }

    /// <summary>
    /// Called when an installment arrives.
    /// </summary>
    /// <param name="wallet">The player's wallet data.</param>
    public void InstallmentArrivalOccurredHandler(WalletData wallet)
    {
        if (wallet.CurrentDigitalMoney >= InstallmentValue)
            InstallmentPaymentMadeHandler(wallet);
        else
            InstallmentPaymentLateHandler(wallet);
        InstallmentArrivalOccurred?.Invoke(Loan);
    }

    /// <summary>
    /// Called when an installment is paid.
    /// </summary>
    /// <param name="wallet">The player's wallet data.</param>
    public void InstallmentPaymentMadeHandler(WalletData wallet)
    {
        if (_remainingPenaltyInstallments != 0)
        {
            wallet.CurrentDigitalMoney -= RemainingPenaltyInstallmentsValue;
            _rawRemainingPenalty -= RemainingPenaltyInstallmentsValue;
            _remainingPenaltyInstallments--;
        }
        else
        {
            wallet.CurrentDigitalMoney -= InstallmentValue;
            _remainingValue -= InstallmentValue;
            _remainingInstallments--;
        }
        InstallmentPaymentMade?.Invoke(Loan);
    }

    /// <summary>
    /// Called when an installment is late.
    /// </summary>
    /// <param name="wallet">The player's wallet data.</param>
    public void InstallmentPaymentLateHandler(WalletData wallet)
    {
        if (_remainingPenaltyInstallments != 0)
        {
            _rawRemainingPenalty -= wallet.CurrentDigitalMoney;
            wallet.CurrentDigitalMoney = 0;
        }
        else
        {
            _remainingValue -= wallet.CurrentDigitalMoney;
            wallet.CurrentDigitalMoney = 0;
            _remainingInstallments--;
        }

        wallet.CurrentDigitalMoney = 0;
        if (_remainingInstallments != 0)
        {
            //Moves the installment value from normal to the penalty one
            _rawRemainingPenalty += InstallmentValue;
            _remainingPenaltyInstallments++;

            _remainingValue -= InstallmentValue;
            _remainingInstallments--;
        }
        else
            _remainingPenaltyInstallments++;

        InstallmentPaymentLate?.Invoke(Loan);
    }

    /// <summary>
    /// Called when a loan is fully repaid by the player.
    /// </summary>
    /// <param name="wallet">The player's wallet data.</param>
    public void LoanFullyRepaidEventHandler(WalletData wallet)
    {
        wallet.CurrentDigitalMoney -= TotalToPay;
        ResetLoanManager();

        PersistenceChanged?.Invoke();
        LoanFullyRepaidEvent?.Invoke(Loan);
    }

    /// <summary>
    /// Called when a loan is granted to the player.
    /// </summary>
    /// <param name="wallet">The player's wallet data.</param>
    public void LoanGrantOccurredHandler(WalletData wallet)
    {
        wallet.CurrentDigitalMoney += Loan.Principal;
        wallet.CurrentDebt += Loan.Total;

        _remainingValue = Loan.Total;
        _remainingInstallments = Loan.Installments;
        _isPersistent = true;
        PersistenceChanged?.Invoke();
        LoanGrantOccurred?.Invoke(Loan);
    }


    /// <summary>
    /// Resets the loan to its initial state and generates a new local random loan.
    /// </summary>
    public void ResetLoanManager()
    {
        _remainingValue = 0;
        _remainingInstallments = 0;
        _remainingPenaltyInstallments = 0;
        _rawRemainingPenalty = 0;
        _isPersistent = false;
        NewLocalRandomLoan();
    }


    /// <summary>
    /// Generates a new local random loan if the current loan is not persistent.
    /// </summary>
    public void NewLocalRandomLoan()
    {
        if (!_isPersistent)
        {
            LoanData _newLoan = GenerateRandomLoan(Loan.LoanType);
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
    public static LoanData GenerateRandomLoan(LoanData.Type type, float minPrincipal = 300, float maxPrincipal = 800, float minRate = 0.05f, float maxRate = 0.15f, int minInstallments = 1, int maxInstallments = 3)
    {
        float _principal = UnityEngine.Random.Range(minPrincipal, maxPrincipal);
        float _rate = UnityEngine.Random.Range(minRate, maxRate);
        int _installments = UnityEngine.Random.Range(minInstallments, maxInstallments + 1);

        return new LoanData(_principal, _rate, _installments, type);
    }
}