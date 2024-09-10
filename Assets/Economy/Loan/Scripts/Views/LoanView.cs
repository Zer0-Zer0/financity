using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        AcceptLoanButtonViewModel acceptLoanButtonViewModel;
        protected override void Setup() { }
        protected override void Configure() { }

        public void SetLoan(LoanData data)
        {
            string _formatedPrincipal = $"{FormatMoney(data.Principal)}";
            string _formatedRate = $"{FormatPercentage(data.Rate)} ({FormatLoanType(data.LoanType)})";
            string _formatedInstallments = $"dividido {data.Installments}X";

            _loanPrincipal.SetText(_formatedPrincipal);
            _loanRate.SetText(_formatedRate);
            _loanInstallments.SetText(_formatedInstallments);

            LoanProcessor loanProcessor = new LoanProcessor(data);

            acceptLoanButtonViewModel.Data = loanProcessor;
        }

        private string FormatLoanType(LoanType loanType)
        {
            switch (loanType)
            {
                case LoanType.SimpleInterest:
                    return "Simples";
                case LoanType.CompoundInterest:
                    return "Composto";
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
            string percentage = (value * 100f).ToString("F2");
            string result = $"{percentage}%";
            return result;
        }
    }
}