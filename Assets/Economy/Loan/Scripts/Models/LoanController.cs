using UnityEngine;
using UISystem;

namespace Economy
{
    public class LoanController : MonoBehaviour
    {
        [SerializeField] GameObject LoanCanvas;
        [SerializeField] LoanViewModel[] loanViewModels;

        public void GenerateRandomLoans()
        {
            LoanCanvas.SetActive(true);
            foreach (var loanViewModel in loanViewModels)
                loanViewModel.ResetLoanView(this, null);
            LoanCanvas.SetActive(false);
        }

        private void Start() => GenerateRandomLoans();
    }
}