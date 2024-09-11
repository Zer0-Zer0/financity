using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public class InstallmentPaymentView : CustomUIComponent
    {
        [Header("Objetos")]
        [SerializeField] Text Titulo;
        [SerializeField] Text Valor;
        [SerializeField] Text Subtitulo;

        [Header("Valores")]
        [SerializeField] string[] FrasesDeEfeito;

        protected override void Setup() { }

        protected override void Configure() { }

        public void UpdateValues()
        {
            float _valor = GetInstallmentValue();
            Debug.Log($"Valor: R${_valor}");
            float _parcelas = GetInstallments();

            Titulo.SetText(SelectRandomPhrase());
            Valor.SetText(FormatMoney(_valor));
            Subtitulo.SetText(FormatSubtitle(_parcelas));
        }

        float GetInstallmentValue()
        {
            float result = 0f;
            if (DataManager.playerData.GetCurrentWallet().Loans.Count != 0)
                result = DataManager.playerData.GetCurrentWallet().Loans[0].InstallmentValue;
            return result;
        }

        float GetInstallments()
        {
            float result;
            if (DataManager.playerData.GetCurrentWallet().Loans.Count != 0)
                result = DataManager.playerData.GetCurrentWallet().Loans[0].TotalRemainingInstallments;
            else
                result = -1;
            return result;
        }

        string SelectRandomPhrase()
        {
            if (FrasesDeEfeito.Length == 0)
            {
                Debug.LogError("Sem frases de efeito para mostrar");
                return "Sem frases de efeito para mostrar";
            }
            int randomIndex = UnityEngine.Random.Range(0, FrasesDeEfeito.Length);
            return FrasesDeEfeito[randomIndex];
        }

        string FormatMoney(float value)
        {
            string result = String.Format("-{0:N2}BRL", value);
            return result;
        }

        string FormatSubtitle(float installments)
        {
            string result;
            if (installments == -1)
                result = $"ERRO: QUANTIA NEGATIVA DE PARCELAS";
            else if (installments == 1)
                result = $"Falta apenas uma parcela!!";
            else if (installments == 0)
                result = $"Você pagou tudo!";
            else
                result = $"Ainda faltam {installments} parcelas!";
            return result;
        }
    }
}
