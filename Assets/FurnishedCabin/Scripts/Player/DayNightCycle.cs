using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;
    public float dayDuration = 60f;
    public Gradient lightColor;
    public AnimationCurve lightIntensity;
    public float initialTime = 0f;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI reportText;
    public TextMeshProUGUI balanceText;
    public RawImage reportImage;

    private float time;
    private DateTime currentDate;
    private string[] daysOfWeek = { "Domingo", "Segunda-Feira", "Terça-Feira", "Quarta-Feira", "Quinta-Feira", "Sexta-Feira", "Sábado" };

    private List<string> transactions = new List<string>();
    private float pendingBalanceChange = 0f;
    private float balance = 0f;

    private FinanceManager financeManager;

    private List<InstallmentPayment> installmentPayments = new List<InstallmentPayment>();

    public MouseVisibilityToggle mouse;

    void Start()
    {
        time = initialTime;
        reportImage.gameObject.SetActive(false);
        UpdateBalanceText(balance);

        financeManager = GetComponent<FinanceManager>();
        if (financeManager == null)
        {
            financeManager = gameObject.AddComponent<FinanceManager>();
        }

        // Define a data inicial
        currentDate = new DateTime(2024, 1, 1);

        // Exemplo de uso para demonstração:
        AddInstallmentPayment("Computador", 1000f, 5, 0.05f); // Adiciona um pagamento parcelado de exemplo
        financeManager.AddPurchase(-800f);
    }

    void Update()
    {
        time += Time.deltaTime / dayDuration;
        if (time >= 1f)
        {
            time = 0f;
            currentDate = currentDate.AddDays(1);
            Debug.Log("New Day: " + currentDate.ToString("dd/MM/yyyy"));
            GenerateReport();
        }

        float sunAngle = time * 360f - 90f;
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);
        directionalLight.color = lightColor.Evaluate(time);
        directionalLight.intensity = lightIntensity.Evaluate(time);

        RenderSettings.ambientLight = lightColor.Evaluate(time) * 0.5f;
        RenderSettings.ambientIntensity = lightIntensity.Evaluate(time) * 0.5f;

        UpdateTimeText();
        UpdateDayText();

        ProcessInstallmentPaymentsDaily(); // Processa as parcelas diariamente
    }

    void UpdateTimeText()
    {
        int hours = Mathf.FloorToInt(time * 24);
        int minutes = Mathf.FloorToInt((time * 24 * 60) % 60);
        string timeString = string.Format("{0:00}:{1:00}", hours, minutes);
        timeText.text = timeString;
    }

    void UpdateDayText()
    {
        string dayOfWeek = currentDate.ToString("dddd", new System.Globalization.CultureInfo("pt-BR"));
        string dateString = currentDate.ToString("dd/MM/yyyy");
        dayText.text = $"{dayOfWeek} - {dateString}";
    }

public void AddTransaction(string description, float amount)
{
    financeManager.AddPurchase(amount);

    // Formata a transação incluindo a data
    string formattedTransaction = string.Format("{0} - {1} = {2} ${3}", currentDate.ToString("dd/MM/yyyy"), description, amount >= 0 ? "+" : "-", Mathf.Abs(amount));
    transactions.Add(formattedTransaction);

    pendingBalanceChange += amount;
}


private void GenerateReport()
{
    // Limpa o texto do relatório antes de gerar um novo relatório
    reportText.text = string.Empty;

    // Variáveis para somar transações de crédito
    float totalCredit = 0f;
    string creditDescription = "Crédito = ";
    bool hasCreditTransactions = false;

    // Variável para somar o valor total das parcelas do dia
    float totalInstallmentAmount = 0f;

    // Atualiza o saldo pendente com base no FinanceManager
    //pendingBalanceChange = financeManager.CurrentBalance;

    // Filtra e mostra todas as transações do dia no relatório
    foreach (string transaction in transactions)
    {
        // Extrai a data da transação para comparar com a currentDate
        string[] parts = transaction.Split(new string[] { " - ", " = " }, StringSplitOptions.None);
        if (parts.Length < 3)
            continue;

        string transactionDateStr = parts[0];
        DateTime transactionDate;
        if (!DateTime.TryParseExact(transactionDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out transactionDate))
            continue;

        // Verifica se a transação ocorreu na currentDate
        if (transactionDate.Date == currentDate.Date)
        {
            // Adiciona a transação ao relatório
            if (parts[1].Contains("Parcela"))
            {
                // Adiciona ao total de parcelas do dia
                float amount = float.Parse(parts[2].TrimStart('$'));
                totalInstallmentAmount += amount;

                // Adiciona a transação de parcela ao relatório
                reportText.text += transaction + "\n";
            }
            else if (parts[1].Contains("Crédito"))
            {
                // Adiciona ao total de crédito e guarda a transação de crédito
                float amount = float.Parse(parts[2].TrimStart('$'));
                totalCredit += amount;
                hasCreditTransactions = true;
            }
            else
            {
                // Outras transações são adicionadas diretamente ao relatório
                reportText.text += transaction + "\n";
            }
        }
    }

    // Adiciona as transações de crédito agrupadas
    if (hasCreditTransactions)
    {
        creditDescription += "-$" + totalCredit.ToString("F2") + "\n";
        reportText.text += creditDescription;
    }

    // Calcula e adiciona o ICMS ao relatório diário
    float icms = pendingBalanceChange * 0.1f;
    reportText.text += "ICMS: $" + icms.ToString("F2") + "\n";

    // Atualiza o total com o ICMS
    pendingBalanceChange += icms;

    // Adiciona o total de parcelas do dia ao relatório
    if (totalInstallmentAmount > 0f)
    {
        reportText.text += $"Total de Parcelas: ${totalInstallmentAmount.ToString("F2")}\n";

        // Adiciona o valor da última parcela processada ao relatório
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

        InstallmentPayment installmentPayment = new InstallmentPayment(description, totalAmount, numInstallments, dailyInterestRate, installmentAmount, currentDate);
        installmentPayments.Add(installmentPayment);
        Debug.Log("Added Installment: " + description + " Total: $" + totalAmount + " Installments: " + numInstallments + " Daily Interest: " + dailyInterestRate);
    }

    private void ProcessInstallmentPaymentsDaily()
{
    foreach (var installmentPayment in installmentPayments)
    {
        if (installmentPayment.remainingInstallments > 0 && currentDate >= installmentPayment.nextPaymentDate)
        {
            float amount = installmentPayment.installmentAmount;

            // Adiciona a transação diária para a parcela
            AddTransaction(installmentPayment.description + " - Parcela", -amount);
            installmentPayment.lastProcessedAmount = amount; // Atualiza o valor da última parcela processada

            // Reduz o número de parcelas restantes
            installmentPayment.remainingInstallments--;

            // Atualiza a próxima data de pagamento
            installmentPayment.nextPaymentDate = installmentPayment.nextPaymentDate.AddDays(1);

            // Se todas as parcelas foram pagas, remove o pagamento parcelado da lista
            if (installmentPayment.remainingInstallments <= 0)
            {
                Debug.Log("Parcelamento concluído para: " + installmentPayment.description);
            }

            // Log para depuração
            Debug.Log("Processed Installment: " + installmentPayment.description + " Amount: $" + amount);
        }
    }

    // Remove os pagamentos parcelados concluídos da lista principal
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
    public float lastProcessedAmount; // Novo campo para armazenar o valor da última parcela processada

    public InstallmentPayment(string desc, float totalAmt, int numInst, float dailyRate, float installmentAmt, DateTime startDate)
    {
        description = desc;
        totalAmount = totalAmt;
        numInstallments = numInst;
        dailyInterestRate = dailyRate;
        installmentAmount = installmentAmt;
        remainingInstallments = numInst;
        nextPaymentDate = startDate.AddDays(1); // Primeira parcela no próximo dia
        lastProcessedAmount = 0f; // Inicializa o valor da última parcela processada como zero
    }
}

}