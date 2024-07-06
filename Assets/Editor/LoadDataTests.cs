using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class LoanDataTests
{
    [Test]
    public void TestCalculatePrincipalFromSimpleInterest()
    {
        float total = 1000f;
        float rate = 0.1f;
        float expectedPrincipal = 909.09f; // Calculated externally

        float calculatedPrincipal = LoanData.CalculatePrincipalFromSimpleInterest(total, rate);

        Assert.AreEqual(expectedPrincipal, calculatedPrincipal, 0.01f); // Allowing for a small error
    }

    [Test]
    public void TestCalculatePrincipalFromCompoundInterest()
    {
        float total = 1000f;
        float rate = 0.1f;
        int installments = 12;
        float expectedPrincipal = 772.18f; // Calculated externally

        float calculatedPrincipal = LoanData.CalculatePrincipalFromCompoundInterest(total, rate, installments);

        Assert.AreEqual(expectedPrincipal, calculatedPrincipal, 0.01f); // Allowing for a small error
    }

    [Test]
    public void TestCalculateTotalFromCompoundInterest()
    {
        float principal = 1000f;
        float rate = 0.1f;
        int installments = 12;
        float expectedTotal = 3138.43f; // Calculated externally

        float calculatedTotal = LoanData.CalculateTotalFromCompoundInterest(principal, rate, installments);

        Assert.AreEqual(expectedTotal, calculatedTotal, 0.01f); // Allowing for a small error
    }

    [Test]
    public void TestCalculateTotalFromSimpleInterest()
    {
        float principal = 1000f;
        float rate = 0.1f;
        float expectedTotal = 1100f; // Calculated externally

        float calculatedTotal = LoanData.CalculateTotalFromSimpleInterest(principal, rate);

        Assert.AreEqual(expectedTotal, calculatedTotal, 0.01f); // Allowing for a small error
    }
}
