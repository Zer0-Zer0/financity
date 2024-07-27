using UnityEngine;

public static class Impostos
{
    public const float ALIQUOTA_IRPF_1 = 0.075f;
    public const float ALIQUOTA_IRPF_2 = 0.15f;
    public const float ALIQUOTA_IRPF_3 = 0.225f;
    public const float ALIQUOTA_IRPF_4 = 0.275f;
    public const float MAXIMO_INSENCAO_IRPF = 1903.99f;
    public const float MAXIMO_FAIXA1_IRPF = 2826.65f;
    public const float MAXIMO_FAIXA2_IRPF = 3751.05f;
    public const float MAXIMO_FAIXA3_IRPF = 4664.68f;


    public static float CalcularIRPF(float salario)
    {
        float imposto = 0f;

        float impostoFaixa1 = (salario - VALOR_MAXIMO_INSENCAO_IRPF) * ALIQUOTA_IRPF_1;
        float impostoFaixa2 = (MAXIMO_FAIXA1_IRPF - VALOR_MAXIMO_INSENCAO_IRPF) * ALIQUOTA_IRPF_1 + (salario - MAXIMO_FAIXA1_IRPF) * ALIQUOTA_IRPF_2;

        float impostoFaixa3 = (MAXIMO_FAIXA1_IRPF - VALOR_MAXIMO_INSENCAO_IRPF) * ALIQUOTA_IRPF_1 + (MAXIMO_FAIXA2_IRPF - MAXIMO_FAIXA1_IRPF) * ALIQUOTA_IRPF_2 + (salario - MAXIMO_FAIXA2_IRPF) * ALIQUOTA_IRPF_3;
        
        float impostoFaixa4 = (MAXIMO_FAIXA1_IRPF - VALOR_MAXIMO_INSENCAO_IRPF) * ALIQUOTA_IRPF_1 + (MAXIMO_FAIXA2_IRPF - MAXIMO_FAIXA1_IRPF) * ALIQUOTA_IRPF_2 + (MAXIMO_FAIXA3_IRPF - MAXIMO_FAIXA2_IRPF) * ALIQUOTA_IRPF_3 + (salario - MAXIMO_FAIXA3_IRPF) * ALIQUOTA_IRPF_4;

        if (salario <= MAXIMO_INSENCAO_IRPF)
        {
            imposto = 0f;
        }
        else if (salario <= MAXIMO_FAIXA1_IRPF)
        {
            imposto = impostoFaixa1;
        }
        else if (salario <= MAXIMO_FAIXA2_IRPF)
        {
            imposto = impostoFaixa2;
        }
        else if (salario <= MAXIMO_FAIXA3_IRPF)
        {
            imposto = impostoFaixa3;
        }
        else
        {
            imposto = impostoFaixa4;
        }

        return imposto;
    }

    // Função para calcular o Imposto de Renda Pessoa Jurídica (IRPJ)
    public float CalcularIRPJ(float lucroMensal)
    {
        float imposto = 0f;

        float lucroAnual = lucroMensal * 12;

        float lucroExcedente = lucroAnual - 20000f;

        if (lucroExcedente <= 0)
        {
            imposto = lucroAnual * 0.15f;
        }
        else
        {
            imposto = 20000f * 0.15f + lucroExcedente * 0.1f;
        }

        return imposto;
    }
}
