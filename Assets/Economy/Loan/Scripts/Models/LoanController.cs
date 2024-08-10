using UnityEngine;

public class LoanController : MonoBehaviour
{
    [SerializeField] LoanProcessor[] loanProcessors;

    public void GenerateRandomLoans()   
    {
        foreach (LoanProcessor loanProcessor in loanProcessors)
            loanProcessor.ResetLoanManager();
    }
}