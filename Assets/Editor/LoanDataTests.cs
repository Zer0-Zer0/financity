using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class LoanDataTests
{
    private float _errorMargin = 0.05f;

    [Test]
    public void TestCalculatePrincipalFromSimpleInterest()
    {
        float total = 1100f;
        float rate = 0.1f;
        float expectedPrincipal = 1000f; // Calculated externally

        float calculatedPrincipal = LoanData.CalculatePrincipalFromSimpleInterest(total, rate);

        Assert.AreEqual(expectedPrincipal, calculatedPrincipal, _errorMargin);
    }

    [Test]
    public void TestCalculatePrincipalFromCompoundInterest()
    {
        float total = 3138.43f;
        float rate = 0.1f;
        int installments = 12;
        float expectedPrincipal = 1000f;

        float calculatedPrincipal = LoanData.CalculatePrincipalFromCompoundInterest(total, rate, installments);

        Assert.AreEqual(expectedPrincipal, calculatedPrincipal, _errorMargin);
    }

    [Test]
    public void TestCalculateTotalFromCompoundInterest()
    {
        float principal = 1000f;
        float rate = 0.1f;
        int installments = 12;
        float expectedTotal = 1761.15f; // Calculated externally

        float calculatedTotal = LoanData.CalculateTotalFromCompoundInterest(principal, rate, installments);

        Assert.AreEqual(expectedTotal, calculatedTotal, _errorMargin);
    }

    [Test]
    public void TestCalculateTotalFromSimpleInterest()
    {
        float principal = 1000f;
        float rate = 0.1f;
        float expectedTotal = 1100f; // Calculated externally

        float calculatedTotal = LoanData.CalculateTotalFromSimpleInterest(principal, rate);

        Assert.AreEqual(expectedTotal, calculatedTotal, _errorMargin);
    }
}
