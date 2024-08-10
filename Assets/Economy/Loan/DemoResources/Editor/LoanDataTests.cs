using System;
using UnityEngine;
using NUnit.Framework;

[TestFixture]
public class LoanDataTests
{
    private float _errorMargin = 0.05f;
    //Site usado para calcular juros compostos: https://www3.bcb.gov.br/CALCIDADAO/publico/calcularFinanciamentoPrestacoesFixas.do

    [Test]
    public void CanConstructLoanDataObject()
    {
        LoanData.Type _loanType = LoanData.Type.CompoundInterest;
        float _principal = 1000f;
        float _rate = 0.1f;
        int _installments = 12;
        Assert.That(() => new LoanData(_principal * -1, _rate, _installments, _loanType), Throws.TypeOf<Exception>());
        Assert.That(() => new LoanData(_principal, _rate * -1, _installments, _loanType), Throws.TypeOf<Exception>());
        Assert.That(() => new LoanData(_principal, _rate * -1, _installments * -1, _loanType), Throws.TypeOf<Exception>());
        LoanData _loanData = new LoanData(_principal, _rate, _installments, _loanType);
    }

    [Test]
    public void CanCalculatePrincipalFromSimpleInterest()
    {
        float total = 1100f;
        float _rate = 0.1f;
        float _expectedPrincipal = 1000f;

        float _calculatedPrincipal = LoanData.CalculatePrincipalFromSimpleInterest(total, _rate);

        Assert.AreEqual(_expectedPrincipal, _calculatedPrincipal, _errorMargin);
    }

    [Test]
    public void CanCalculatePrincipalFromCompoundInterest()
    {
        float total = 3138.43f;
        float _rate = 0.1f;
        int _installments = 12;
        float _expectedPrincipal = 1000f;

        float _calculatedPrincipal = LoanData.CalculatePrincipalFromCompoundInterest(total, _rate, _installments);

        Assert.AreEqual(_expectedPrincipal, _calculatedPrincipal, _errorMargin);
    }

    [Test]
    public void CanCalculateTotalFromCompoundInterest()
    {
        float _principal = 1000f;
        float _rate = 0.1f;
        int _installments = 12;
        float _expectedTotal = 1761.15f;

        float _calculatedTotal = LoanData.CalculateTotalFromCompoundInterest(_principal, _rate, _installments);

        Assert.AreEqual(_expectedTotal, _calculatedTotal, _errorMargin);
    }

    [Test]
    public void CanCalculateTotalFromSimpleInterest()
    {
        float _principal = 1000f;
        float _rate = 0.1f;
        float _expectedTotal = 1100f;

        float _calculatedTotal = LoanData.CalculateTotalFromSimpleInterest(_principal, _rate);

        Assert.AreEqual(_expectedTotal, _calculatedTotal, _errorMargin);
    }

    [Test]
    public void TestInstallmentValue()
    {
        LoanData.Type _loanType = LoanData.Type.CompoundInterest;
        float _principal = 1000f;
        float _rate = 0.1f;
        int _installments = 12;

        LoanData _loanData = new LoanData(_principal, _rate, _installments, _loanType);

        float _expectedResult = 146.76f;
        float _result = _loanData.InstallmentValue;

        Assert.AreEqual(_expectedResult, _result, _errorMargin);
    }
}
