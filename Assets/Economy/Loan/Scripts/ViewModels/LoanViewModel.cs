using UnityEngine;


namespace UISystem
{
    public class LoanViewModel : MonoBehaviour
    {
        [SerializeField] LoanView loanView;

        public void LoanChanged(Component sender, object data){
            if(data is LoanData loanData)
                loanView.SetLoan(loanData);
        }
    }
}