using NUnit.Framework;
using UnityEngine;

public class LoanManagerTests
{
    private WalletData _walletData;
    private GameObject _testObject;
    private LoanProcessor _loanManager;

    [SetUp]
    public void Setup()
    {
        _loanManager = new LoanProcessor();
        _walletData = ScriptableObject.CreateInstance<WalletData>();
        _walletData.CurrentDigitalMoney = 0f;
        _walletData.CurrentPhysicalMoney = 0f;
        _walletData.CurrentMaxDebt = 1000f;
        _walletData.CurrentDebt = 0f;
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

        LoanData randomLoan = LoanProcessor.GenerateRandomLoan(_loanType, _minPrincipal, _maxPrincipal, _minRate, _maxRate, _minInstallments, _maxInstallments);

        Assert.GreaterOrEqual(randomLoan.Principal, _minPrincipal);
        Assert.LessOrEqual(randomLoan.Principal, _maxPrincipal);
        Assert.GreaterOrEqual(randomLoan.Rate, _minRate);
        Assert.LessOrEqual(randomLoan.Rate, _maxRate);
        Assert.GreaterOrEqual(randomLoan.Installments, _minInstallments);
        Assert.LessOrEqual(randomLoan.Installments, _maxInstallments);
        Assert.AreEqual(_loanType, randomLoan.LoanType);
    }


    [Test]
    public void InstallmentArrivalOccurredHandler_EnoughMoney_PaysInstallment()
    {
        // Set up scenario
        _walletData.CurrentDigitalMoney = 500f;
        LoanData loan = new LoanData(500f, 0.1f, 1, LoanData.Type.SimpleInterest);
        _loanManager.SetLoanData(loan);
        _loanManager.LoanGrantOccurredHandler(_walletData);//Initializes loan


        // Call method
        _loanManager.InstallmentArrivalOccurredHandler(_walletData);

        // Assert
        Assert.AreEqual(450f, _walletData.CurrentDigitalMoney);
    }

    [Test]
    public void InstallmentArrivalOccurredHandler_NotEnoughMoney_PaysPenalty()
    {
        // Set up scenario
        LoanData loan = new LoanData(500f, 0.1f, 1, LoanData.Type.SimpleInterest);
        _loanManager.SetLoanData(loan);
        _loanManager.LoanGrantOccurredHandler(_walletData);

        _walletData.CurrentDigitalMoney = 100f;

        // Call method
        _loanManager.InstallmentArrivalOccurredHandler(_walletData);

        // Assert
        Assert.AreEqual(0f, _walletData.CurrentDigitalMoney);
    }

    [Test]
    public void InstallmentPaymentMadeHandler_PaysInstallment()
    {
        // Test making a regular installment payment
    }

    [Test]
    public void InstallmentPaymentMadeHandler_PaysPenalty()
    {
        // Test making a penalty installment payment
    }

    [Test]
    public void InstallmentPaymentLateHandler_MovesToPenalty()
    {
        // Test moving installment to penalty when payment is late
    }

    [Test]
    public void InstallmentPaymentLateHandler_AddsToPenalty()
    {
        // Test adding to penalty when payment is late
    }

    [Test]
    public void LoanFullyRepaidEventHandler_FullyRepayLoan()
    {
        // Test fully repaying a loan
    }

    [Test]
    public void LoanGrantOccurredHandler_GrantLoan()
    {
        // Test granting a new loan
    }

    [Test]
    public void NewLocalRandomLoan_GeneratesNewLoan()
    {
        // Test generating a new random loan
    }

    [Test]
    public void ResetLoanManager_ResetsLoan()
    {
        // Test resetting the loan manager
    }

    [Test]
    public void GenerateRandomLoan_CreatesRandomLoan()
    {
        // Test generating a random loan with specified parameters
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up resources or reset state after each test
        _walletData = null;
        _loanManager = null;
        GameObject.DestroyImmediate(_testObject);
    }
}