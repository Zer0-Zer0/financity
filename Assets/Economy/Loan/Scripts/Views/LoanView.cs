using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class LoanView : CustomUIComponent
    {
        [SerializeField]
        Text _loanPrincipal;

        [SerializeField]
        Text _loanRate;

        [SerializeField]
        Text _loanInstallments;

        [SerializeField]
        Text _loanType;
        protected override void Setup() { }
        protected override void Configure() { }

        public void SetLoan(LoanData data)
        {
            string _formatedPrincipal = $"{FormatMoney(data.Principal)}";
            string _formatedRate = $"Taxa de juros (ao dia): {FormatPercentage(data.Rate)}";
            string _formatedInstallments = $"Parcelas: {data.Installments}";
            string _formatedLoanType = $"Tipo de emprestimo {data.LoanType.ToString()}";

            _loanPrincipal.SetText(_formatedPrincipal);
            _loanRate.SetText(_formatedRate);
            _loanInstallments.SetText(_formatedInstallments);
            _loanType.SetText(_formatedLoanType);
        }

        private string FormatMoney(float value)
        {
            string result = String.Format("{0:N2}BRL", value.ToString());
            return result;
        }

        private string FormatPercentage(float value){
            string result = value.ToString("P");
            return result;
        }
    }
}