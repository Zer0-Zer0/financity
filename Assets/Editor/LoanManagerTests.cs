using NUnit.Framework;
using UnityEngine;

public class LoanManagerTests
{
    private WalletData _walletData;

    [SetUp]
    public void Setup()
    {
        _walletData = ScriptableObject.CreateInstance<WalletData>();
    }


    [Test]
    public void TestGenerateRandomLoan()
    {
        LoanData.Type _loanType = LoanData.Type.SimpleInterest;
        float _minPrincipal = 300f;
        float _maxPrincipal = 800f;
        float _minRate = 0.05f;
        float _maxRate = 0.15f;
        int _minInstallments = 1;
        int _maxInstallments = 3;

        LoanData randomLoan = LoanManager.GenerateRandomLoan(_loanType, _minPrincipal, _maxPrincipal, _minRate, _maxRate, _minInstallments, _maxInstallments);

        Assert.GreaterOrEqual(randomLoan.Principal, _minPrincipal);
        Assert.LessOrEqual(randomLoan.Principal, _maxPrincipal);
        Assert.GreaterOrEqual(randomLoan.Rate, _minRate);
        Assert.LessOrEqual(randomLoan.Rate, _maxRate);
        Assert.GreaterOrEqual(randomLoan.Installments, _minInstallments);
        Assert.LessOrEqual(randomLoan.Installments, _maxInstallments);
        Assert.AreEqual(_loanType, randomLoan._LoanType);
    }

    [UnityTest]
    public IEnumerator InstallmentArrivalOccurredHandler_EnoughMoney_PaysInstallment()
    {
        // Test paying installment with enough money
    }

    [UnityTest]
    public IEnumerator InstallmentArrivalOccurredHandler_NotEnoughMoney_PaysPenalty()
    {
        // Test paying penalty when there is not enough money
    }

    [UnityTest]
    public IEnumerator InstallmentPaymentMadeHandler_PaysInstallment()
    {
        // Test making a regular installment payment
    }

    [UnityTest]
    public IEnumerator InstallmentPaymentMadeHandler_PaysPenalty()
    {
        // Test making a penalty installment payment
    }

    [UnityTest]
    public IEnumerator InstallmentPaymentLateHandler_MovesToPenalty()
    {
        // Test moving installment to penalty when payment is late
    }

    [UnityTest]
    public IEnumerator InstallmentPaymentLateHandler_AddsToPenalty()
    {
        // Test adding to penalty when payment is late
    }

    [UnityTest]
    public IEnumerator LoanFullyRepaidEventHandler_FullyRepayLoan()
    {
        // Test fully repaying a loan
    }

    [UnityTest]
    public IEnumerator LoanGrantOccurredHandler_GrantLoan()
    {
        // Test granting a new loan
    }

    [UnityTest]
    public IEnumerator NewLocalRandomLoan_GeneratesNewLoan()
    {
        // Test generating a new random loan
    }

    [UnityTest]
    public IEnumerator ResetLoanManager_ResetsLoan()
    {
        // Test resetting the loan manager
    }

    [UnityTest]
    public IEnumerator GenerateRandomLoan_CreatesRandomLoan()
    {
        // Test generating a random loan with specified parameters
    }
}

