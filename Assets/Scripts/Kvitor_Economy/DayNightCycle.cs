using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] TimeManager timeManager;
    public TextMeshProUGUI reportText;
    public TextMeshProUGUI balanceText;
    public RawImage reportImage;

    private List<string> transactions = new List<string>();
    private float pendingBalanceChange = 0f;
    private float balance = 0f;

    private FinanceManager financeManager;

    private List<InstallmentPayment> installmentPayments = new List<InstallmentPayment>();

    public MouseVisibilityToggle mouse;

    void Start()
    {
        reportImage.gameObject.SetActive(false);
        UpdateBalanceText(balance);

        financeManager = GetComponent<FinanceManager>();
        if (financeManager == null)
        {
            financeManager = gameObject.AddComponent<FinanceManager>();
        }

        AddInstallmentPayment("Computador", 1000f, 5, 0.05f);
        financeManager.AddPurchase(-800f);
    }

    void DayPassed(){
        GenerateReport();
    }

    void Update()
    {
        ProcessInstallmentPaymentsDaily();
    }

    public void AddTransaction(string description, float amount)
    {
        financeManager.AddPurchase(amount);
        string formattedTransaction = string.Format("{0} - {1} = {2} ${3}", timeManager.CurrentDate.ToString("dd/MM/yyyy"), description, amount >= 0 ? "+" : "-", Mathf.Abs(amount));
        transactions.Add(formattedTransaction);
        pendingBalanceChange += amount;
    }

    private void GenerateReport()
    {
        reportText.text = string.Empty;

        float totalCredit = 0f;
        string creditDescription = "Crédito = ";
        bool hasCreditTransactions = false;

        float totalInstallmentAmount = 0f;

        pendingBalanceChange = financeManager.GetCurrentBalance();

        foreach (string transaction in transactions)
        {
            string[] parts = transaction.Split(new string[] { " - ", " = " }, StringSplitOptions.None);
            if (parts.Length < 3)
                continue;

            string transactionDateStr = parts[0];
            DateTime transactionDate;
            if (!DateTime.TryParseExact(transactionDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out transactionDate))
                continue;

            if (transactionDate.Date == timeManager.CurrentDate.Date)
            {
                if (parts[1].Contains("Parcela"))
                {
                    float amount = float.Parse(parts[2].TrimStart('$'));
                    totalInstallmentAmount += amount;
                    reportText.text += transaction + "\n";
                }
                else if (parts[1].Contains("Crédito"))
                {
                    float amount = float.Parse(parts[2].TrimStart('$'));
                    totalCredit += amount;
                    hasCreditTransactions = true;
                }
                else
                {
                    reportText.text += transaction + "\n";
                }
            }
        }

        if (hasCreditTransactions)
        {
            creditDescription += "-$" + totalCredit.ToString("F2") + "\n";
            reportText.text += creditDescription;
        }

        float icms = pendingBalanceChange * 0.1f;
        reportText.text += "ICMS: $" + icms.ToString("F2") + "\n";
        pendingBalanceChange += icms;

        if (totalInstallmentAmount > 0f)
        {
            reportText.text += $"Total de Parcelas: ${totalInstallmentAmount.ToString("F2")}\n";
            foreach (var installmentPayment in installmentPayments)
            {
                if (installmentPayment.lastProcessedAmount > 0f)
                {
                    reportText.text += string.Format("Parcela Processada: {0} - ${1}\n", installmentPayment.description, installmentPayment.lastProcessedAmount.ToString("F2"));
                }
            }
        }

        reportText.text += "----------------------------\n";
        reportText.text += string.Format("Total: {0} ${1}", pendingBalanceChange >= 0 ? "+" : "-", Mathf.Abs(pendingBalanceChange));

        reportImage.gameObject.SetActive(true);
        mouse.ToggleMouse();
    }

    public void HideReport()
    {
        float oldBalance = balance;
        balance += pendingBalanceChange;
        AnimateBalanceChange(oldBalance, balance, pendingBalanceChange >= 0);

        reportImage.gameObject.SetActive(false);
        transactions.Clear();
        pendingBalanceChange = 0f;
        mouse.ToggleMouse();
    }

    public void AddMoney(float amount)
    {
        float oldBalance = balance;
        balance += amount;
        AnimateBalanceChange(oldBalance, balance, amount >= 0);
    }

    private void AnimateBalanceChange(float oldBalance, float newBalance, bool positiveChange)
    {
        Color targetColor = positiveChange ? Color.white : Color.red;
        StartCoroutine(AnimateText(balanceText, oldBalance, newBalance, targetColor));
    }

    private IEnumerator AnimateText(TextMeshProUGUI text, float startValue, float endValue, Color targetColor)
    {
        float duration = 1f;
        float elapsedTime = 0f;
        Color initialColor = text.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float value = Mathf.Lerp(startValue, endValue, t);
            text.text = "$" + value.ToString("F2");
            text.color = Color.Lerp(initialColor, targetColor, t);
            yield return null;
        }

        text.text = "$" + endValue.ToString("F2");
        text.color = Color.white;
    }

    private void UpdateBalanceText(float newBalance)
    {
        balanceText.text = "$" + newBalance.ToString("F2");
    }

    public void AddInstallmentPayment(string description, float totalAmount, int numInstallments, float dailyInterestRate)
    {
        float installmentAmount = CalculateInstallmentAmount(totalAmount, numInstallments, dailyInterestRate);

        InstallmentPayment installmentPayment = new InstallmentPayment(description, totalAmount, numInstallments, dailyInterestRate, installmentAmount, timeManager.CurrentDate);
        installmentPayments.Add(installmentPayment);
        Debug.Log("Added Installment: " + description + " Total: $" + totalAmount + " Installments: " + numInstallments + " Daily Interest: " + dailyInterestRate);
    }

    private void ProcessInstallmentPaymentsDaily()
    {
        foreach (var installmentPayment in installmentPayments)
        {
            if (installmentPayment.remainingInstallments > 0 && timeManager.CurrentDate >= installmentPayment.nextPaymentDate)
            {
                float amount = installmentPayment.installmentAmount;
                AddTransaction(installmentPayment.description + " - Parcela", -amount);
                installmentPayment.lastProcessedAmount = amount;
                installmentPayment.remainingInstallments--;
                installmentPayment.nextPaymentDate = installmentPayment.nextPaymentDate.AddDays(1);

                if (installmentPayment.remainingInstallments <= 0)
                {
                    Debug.Log("Parcelamento concluído para: " + installmentPayment.description);
                }

                Debug.Log("Processed Installment: " + installmentPayment.description + " Amount: $" + amount);
            }
        }

        installmentPayments.RemoveAll(installment => installment.remainingInstallments <= 0);
    }

    private float CalculateInstallmentAmount(float totalAmount, int numInstallments, float dailyInterestRate)
    {
        float interestFactor = Mathf.Pow(1 + dailyInterestRate, numInstallments);
        float installmentAmount = totalAmount * (interestFactor * dailyInterestRate) / (interestFactor - 1);
        return installmentAmount;
    }

    private class InstallmentPayment
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
            description = desc;
            totalAmount = totalAmt;
            numInstallments = numInst;
            dailyInterestRate = dailyRate;
            installmentAmount = installmentAmt;
            remainingInstallments = numInst;
            nextPaymentDate = startDate.AddDays(1);
            lastProcessedAmount = 0f;
        }
    }
}