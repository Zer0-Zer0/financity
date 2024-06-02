using System;
using UnityEngine;
public class LoanManager : MonoBehaviour
{
    /// <summary>
    /// Makes a loan for the player, by adding money and debt to it's account
    /// </summary>
    /// <param name="wallet">The wallet manager of the player.</param>
    /// <param name="loanData">The data of the loan to be made.</param>
    /// <remarks>
    /// If the new debt exceeds the current maximum debt, an error is logged.
    /// </remarks>
    public static void MakeALoan(WalletManager wallet, LoanData loanData)
    {
        try
        {
            float newDebt = wallet.CurrentDebt + loanData.Total;

            if (newDebt > wallet.CurrentMaxDebt)
            {
                throw new Exception("New debt exceeds the current maximum debt");
            }

            wallet.CurrentDebt = newDebt;
            wallet.CurrentDigitalMoney += loanData.Principal;
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
    public static LoanData PayAInstallment(WalletManager wallet, LoanData loanData)
    {
        try
        {
            if (loanData.GetInstallmentValue() > wallet.CurrentDigitalMoney)
            {
                throw new Exception("Insufficient digital money to pay the installment");
            }

            wallet.CurrentDebt -= loanData.GetInstallmentValue();
            loanData.RemainingValue -= loanData.GetInstallmentValue();
            loanData.RemainingInstallments--;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error paying a installment: " + ex.Message);
        }

        return loanData;
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
        int _installments = UnityEngine.Random.Range(minInstallments, maxInstallments);

        return new LoanData(_principal, _rate, _installments, type);
    }
}