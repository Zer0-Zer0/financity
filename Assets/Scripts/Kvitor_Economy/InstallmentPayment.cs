using System.Collections.Generic;
using System.Collections;
using System;
public class InstallmentPayment
{
    public string description;
    public float totalAmount;
    public int numInstallments;
    public float dailyInterestRate;
    public float installmentAmount;
    public int remainingInstallments;
    public DateTime nextPaymentDate;
    public float lastProcessedAmount;

    public InstallmentPayment(string desc, float totalAmt, int numInst, float dailyRate, float installmentAmt, DateTime startDate)
    {
        this.description = desc;
        this.totalAmount = totalAmt;
        this.numInstallments = numInst;
        this.dailyInterestRate = dailyRate;
        this.installmentAmount = installmentAmt;
        this.remainingInstallments = numInst;
        this.nextPaymentDate = startDate.AddDays(1);
        this.lastProcessedAmount = 0f;
    }
}