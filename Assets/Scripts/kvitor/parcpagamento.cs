using System.Collections.Generic;
using UnityEngine;

public class ParcelaManager : MonoBehaviour
{
    private List<Parcela> parcelas = new List<Parcela>();

    public void AdicionarParcela(float valorTotal, int numeroDeParcelas)
    {
        float valorDiario = (valorTotal / numeroDeParcelas) / 30f;
        parcelas.Add(new Parcela(valorDiario, numeroDeParcelas));
    }

    public float CalcularValorDiarioTotal()
    {
        float valorDiarioTotal = 0f;
        foreach (Parcela parcela in parcelas)
        {
            valorDiarioTotal += parcela.ValorDiario;
        }
        return valorDiarioTotal;
    }
}

public class Parcela
{
    public float ValorDiario { get; private set; }
    public int NumeroDeParcelas { get; private set; }

    public Parcela(float valorDiario, int numeroDeParcelas)
    {
        ValorDiario = valorDiario;
        NumeroDeParcelas = numeroDeParcelas;
    }
}
