using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System;

/// <summary>
/// Manages the day-night cycle in the game.
/// </summary>
[RequireComponent(typeof(FinanceManager))]
[RequireComponent(typeof(TimeManager))]
[RequireComponent(typeof(MouseVisibilityToggle))]
[RequireComponent(typeof(WalletManager))]
public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI reportText;
    [SerializeField] private TextMeshProUGUI balanceText;
    [SerializeField] private RawImage reportImage;
    

    private List<string> transactions = new List<string>();
    private float pendingBalanceChange = 0f;
    private float balance = 0f;

    private WalletManager walletManager;
    private TimeManager timeManager;
    private FinanceManager financeManager;
    private MouseVisibilityToggle mouseVisibilityToggle;

    private List<InstallmentPayment> installmentPayments = new List<InstallmentPayment>();

    void Awake()
    {
        financeManager = GetComponent<FinanceManager>();
        timeManager = GetComponent<TimeManager>();
        mouseVisibilityToggle = GetComponent<MouseVisibilityToggle>();
    }

    void Start()
    {
        timeManager.OnDayPassedEvent.AddListener(DayPassed);

        reportImage.gameObject.SetActive(false);
        UpdateBalanceText(balance);

        AddInstallmentPayment("Computador", 1000f, 5, 0.05f);
        financeManager.AddPurchase(-800f);
    }

    void DayPassed(EventObject _){
        GenerateReport();
    }

    void Update()
    {
        ProcessInstallmentPaymentsDaily();
    }

    /// <summary>
    /// Adds a transaction to the list of transactions.
    /// </summary>
    /// <param name="description">Description of the transaction.</param>
    /// <param name="amount">Amount of the transaction.</param>
    public void AddTransaction(string description, float amount)
    {
        financeManager.AddPurchase(amount);
        string formattedTransaction = string.Format("{0} - {1} = {2} ${3}", timeManager.currentDate.ToString("dd/MM/yyyy"), description, amount >= 0 ? "+" : "-", Mathf.Abs(amount));
        transactions.Add(formattedTransaction);
        pendingBalanceChange += amount;
    }

    /// <summary>
    /// Generates the daily financial report.
    /// </summary>
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

            if (transactionDate.Date == timeManager.currentDate.Date)
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
        mouseVisibilityToggle.ToggleMouse();
    }

    /// <summary>
    /// Hides the financial report.
    /// </summary>
    public void HideReport()
    {
        float oldBalance = balance;
        balance += pendingBalanceChange;
        AnimateBalanceChange(oldBalance, balance, pendingBalanceChange >= 0);

        reportImage.gameObject.SetActive(false);
        transactions.Clear();
        pendingBalanceChange = 0f;
        mouseVisibilityToggle.ToggleMouse();
    }

    /// <summary>
    /// Adds money to the balance.
    /// </summary>
    /// <param name="amount">Amount to add.</param>
    public void AddMoney(float amount)
    {
        float oldBalance = balance;
        balance += amount;
        AnimateBalanceChange(oldBalance, balance, amount >= 0);
    }

    /// <summary>
    /// Animates the balance change.
    /// </summary>
    private void AnimateBalanceChange(float oldBalance, float newBalance, bool positiveChange)
    {
        Color targetColor = positiveChange ? Color.white : Color.red;
        StartCoroutine(AnimateText(balanceText, oldBalance, newBalance, targetColor));
    }

    /// <summary>
    /// Animates the text change.
    /// </summary>
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

    /// <summary>
    /// Updates the balance text.
    /// </summary>
    private void UpdateBalanceText(float newBalance)
    {
        balanceText.text = "$" + newBalance.ToString("F2");
    }

    /// <summary>
    /// Adds an installment payment.
    /// </summary>
    public void AddInstallmentPayment(string description, float totalAmount, int numInstallments, float dailyInterestRate)
    {
        float installmentAmount = CalculateInstallmentAmount(totalAmount, numInstallments, dailyInterestRate);

        InstallmentPayment installmentPayment = new InstallmentPayment(description, totalAmount, numInstallments, dailyInterestRate, installmentAmount, timeManager.currentDate);
        installmentPayments.Add(installmentPayment);
        Debug.Log("Added Installment: " + description + " Total: $" + totalAmount + " Installments: " + numInstallments + " Daily Interest: " + dailyInterestRate);
    }

    /// <summary>
    /// Processes installment payments daily.
    /// </summary>
    private void ProcessInstallmentPaymentsDaily()
    {
        foreach (var installmentPayment in installmentPayments)
        {
            if (installmentPayment.remainingInstallments > 0 && timeManager.currentDate >= installmentPayment.nextPaymentDate)
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

    private float CalculateInstallmentAmount(float principalAmount, int numberOfInstallments, float dailyInterestRate)
    {
        float interestFactor = Mathf.Pow(1 + dailyInterestRate, numberOfInstallments);
        float monthlyInterestRate = dailyInterestRate;
        float installmentAmount = principalAmount * (interestFactor * monthlyInterestRate) / (interestFactor - 1);
        return installmentAmount;
    }


}
