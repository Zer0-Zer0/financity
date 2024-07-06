using NUnit.Framework;
using UnityEngine;

public class LoanManagerTests
{
    [Test]
    public void TestGenerateRandomLoan()
    {
        LoanData.Type loanType = LoanData.Type.SimpleInterest;
        float minPrincipal = 300f;
        float maxPrincipal = 800f;
        float minRate = 0.05f;
        float maxRate = 0.15f;
        int minInstallments = 1;
        int maxInstallments = 3;

        LoanData randomLoan = LoanManager.GenerateRandomLoan(loanType, minPrincipal, maxPrincipal, minRate, maxRate, minInstallments, maxInstallments);

        Assert.GreaterOrEqual(randomLoan.Principal, minPrincipal);
        Assert.LessOrEqual(randomLoan.Principal, maxPrincipal);
        Assert.GreaterOrEqual(randomLoan.Rate, minRate);
        Assert.LessOrEqual(randomLoan.Rate, maxRate);
        Assert.GreaterOrEqual(randomLoan.Installments, minInstallments);
        Assert.LessOrEqual(randomLoan.Installments, maxInstallments);
        Assert.AreEqual(loanType, randomLoan.LoanType);
    }
}
