using UnityEngine;
using Economy;
using UISystem;

public class LoanViewModel : MonoBehaviour
{
    [SerializeField] LoanView loanView;
    [SerializeField] LoanProcessor loanProcessor;
    public void LoanChanged(Component sender, object data)
    {
        if (data is LoanData loanData)
        {
            loanProcessor.Loan = loanData;
            loanView.SetLoan(loanData);
        }
    }
    public void ResetLoanView(Component sender, object data)
    {
        loanProcessor.ResetLoanProcessor();
        loanView.SetLoan(loanProcessor.Loan);
    }
}