using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
public class LoanManager: MonoBehaviour
{
    public static UnityEvent<LoanData> LoanAccepted;
    public static UnityEvent<LoanData> LoanPaid;
    public static UnityEvent<LoanData> InstallmentPaid;
    public static UnityEvent<LoanData> InstallmentArrived;
    public static UnityEvent<LoanData> InstallmentLate;
    public UnityEvent PersistenseChanged;

    LoanData _loan;

    float _remainingValue;
    float _remainingInstallments;
    float _installmentValue{
        get{
            return _remainingValue / _remainingInstallments;
        }
    }

    float _remainingFine;
    float _remainingLateInstallments;
    float _lateInstallmentRate;

    public void SetLoanData(LoanData Loan){
        _loan = Loan;
    }

    public void OnInstallmentArrival(WalletData wallet){
        if (wallet.CurrentDigitalMoney >= _installmentValue){
            OnInstallmentPaid(wallet);
        }else{
            OnInstallmentLate(wallet);
        }
    }

    public void OnInstallmentPaid(WalletData wallet){
        //Lógica de quando parcela é paga com sucesso
        if ()
        wallet.CurrentDigitalMoney -= _installmentValue;
        _remainingInstallments--;
    }

    public void OnInstallmentLate(WalletData wallet){
        //Lógica de quando há falha no pagamento da parcela
    }

    public void OnLoanPaid(WalletData wallet){
        //Lógica de quando empréstimo é pago com sucesso
    }

    public void OnLoanAccepted(WalletData wallet){
        //Lógica de quando empréstimo é aceito
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

    public void NewLocalRandomLoan(){
        _loan = RandomLoan(_loan.LoanType);
        Debug.Log(_loan.ToString());
    }
}