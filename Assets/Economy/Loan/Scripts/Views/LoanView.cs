using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Economy;

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

        [SerializeField]
        AcceptLoanButtonViewModel acceptLoanButtonViewModel;
        protected override void Setup() { }
        protected override void Configure() { }

        public void SetLoan(LoanData data)
        {
            string _formatedPrincipal = $"{FormatMoney(data.Principal)}";
            string _formatedRate = $"{FormatPercentage(data.Rate)}(ao dia)";
            string _formatedInstallments = $"{data.Installments} Parcelas";
            string _formatedLoanType = $"{FormatLoanType(data.LoanType)}";

            _loanPrincipal.SetText(_formatedPrincipal);
            _loanRate.SetText(_formatedRate);
            _loanInstallments.SetText(_formatedInstallments);
            _loanType.SetText(_formatedLoanType);

            LoanProcessor loanProcessor = new LoanProcessor(data);

            acceptLoanButtonViewModel.Data = loanProcessor;
        }

        private string FormatLoanType(LoanData.Type loanType)
        {
            switch (loanType)
            {
                case LoanData.Type.SimpleInterest:
                    return "Juros Simples";
                case LoanData.Type.CompoundInterest:
                    return "Juros Composto";
                default:
                    throw new Exception("ERROR: Not implemented loan.");
            }
        }

        private string FormatMoney(float value)
        {
            string result = String.Format("{0:N2}BRL", value);
            return result;
        }

        private string FormatPercentage(float value)
        {
            string result = value.ToString("P");
            return result;
        }
    }
}