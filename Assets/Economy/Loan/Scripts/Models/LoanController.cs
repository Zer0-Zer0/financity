using UnityEngine;
using UISystem;

namespace Economy
{
    public class LoanController : MonoBehaviour
    {
        [SerializeField] LoanViewModel[] loanViewModels;

        public void GenerateRandomLoans()
        {
            foreach (var loanViewModel in loanViewModels)
                loanViewModel.ResetLoanView(this, null);
        }

        private void OnEnable() => GenerateRandomLoans();
    }
}