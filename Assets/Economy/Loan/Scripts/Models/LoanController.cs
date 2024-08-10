using UnityEngine;
using UISystem;

public class LoanController : MonoBehaviour
{
    [SerializeField] LoanViewModel[] loanViewModels;

    public void GenerateRandomLoans()
    {
        foreach (LoanViewModel loanViewModel in loanViewModels)
            loanViewModel.ResetLoanView(this, null);
    }

    private void OnEnable() => GenerateRandomLoans();
}