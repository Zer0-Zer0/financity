using System;
using UnityEngine;

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
    /// Represents data for a loan, including total value, principal value, interest rate, installment value, and installments value.
    /// </summary>
    /// <param name="Total">The total value of the loan.</param>
    /// <param name="Principal">The principal value of the loan.</param>
    /// <param name="Rate">The interest rate of the loan, in percent.</param>
    /// <param name="Installments">The installments duration of the loan.</param>
    /// <param name="loanType">The type of loan.</param>
    [System.Serializable]
    public struct LoanData{
        public readonly float Total;
        public readonly float Principal;
        public readonly float Rate;
        public readonly int Installments;
        public readonly LoanType loanType;

        private float _remainingValue;
        public float RemainingValue{
            get{
                return _remainingValue;
            }
            set{
                _remainingValue = value;
            }
        }

        private float _remainingInstallments;
        public float RemainingInstallments{
            get{
                return _remainingInstallments;
            }
            set{
                _remainingInstallments = value;
            }
        }

        public LoanData(float principal, float rate, int installments, LoanType type, float total = 0)
        {
            Total = total;
            if (total == 0){
                switch(type){
                    case LoanType.SimpleInterest:
                    Total = CalculateSimpleInterest(principal, rate);
                    break;
                    case LoanType.CompoundInterest:
                    Total = CalculateCompoundInterest(principal, rate, installments);
                    break;
                }
            }
            _remainingValue = total;
            Principal = principal;
            Rate = rate;
            Installments = installments;
            loanType = type;
            _remainingInstallments= installments;
        }

        /// <summary>
        /// Calculates the installment amount for the loan.
        /// </summary>
        /// <returns>The installment amount for the loan.</returns>
        internal float GetInstallmentValue(){
            return Total/Installments;
        }

        /// <summary>
        /// Calculates the compound interest based on the principal, rate, and installments.
        /// </summary>
        /// <param name="principal">The loaned amount of money, without the interest.</param>
        /// <param name="rate">The interest rate per period, in percent.</param>
        /// <param name="installments">The number of periods the interest is applied.</param>
        /// <returns>A LoanData struct with the calculated values after applying compound interest.</returns>
        public static float CalculateCompoundInterest(float principal, float rate, int installments){
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
        public static float CalculateSimpleInterest(float principal, float rate){
            float _calculatedRate = rate + 1;
            float _total = principal * _calculatedRate;
            return _total;
        }
    }

    /// <summary>
    /// Makes a loan for the player, by adding money and debt to it's account
    /// </summary>
    /// <param name="wallet">The wallet manager of the player.</param>
    /// <param name="loanData">The data of the loan to be made.</param>
    /// <remarks>
    /// If the new debt exceeds the current maximum debt, an error is logged.
    /// </remarks>
    public static void MakeALoan(WalletManager wallet, LoanData loanData){
        try
        {
        float newDebt = wallet.CurrentDebt + loanData.Total;

        if(newDebt > wallet.CurrentMaxDebt){
            throw new Exception("New debt exceeds the current maximum debt");
        }

        wallet.CurrentDebt = newDebt;
        wallet.CurrentDigitalMoney += loanData.Principal; 
        }catch (Exception ex){
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
    public static LoanData PayAInstallment(WalletManager wallet, LoanData loanData){
        try{
            if (loanData.GetInstallmentValue() > wallet.CurrentDigitalMoney){
                throw new Exception("Insufficient digital money to pay the installment");
            }

            wallet.CurrentDebt -= loanData.GetInstallmentValue();
            loanData.RemainingValue -= loanData.GetInstallmentValue();
            loanData.RemainingInstallments --;
            }catch (Exception ex){
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
    public static LoanData RandomLoan(LoanType type, float minPrincipal = 300, float maxPrincipal = 800, float minRate = 5, float maxRate = 15, int minInstallments = 1, int maxInstallments = 3){
        float _principal = UnityEngine.Random.Range(minPrincipal, maxPrincipal);
        float _rate = UnityEngine.Random.Range(minRate,maxRate);
        int _installments = UnityEngine.Random.Range(minInstallments, maxInstallments);

        return new LoanData(_principal, _rate, _installments, type);
    }
}