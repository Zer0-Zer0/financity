using UnityEngine;
using TMPro;

public class Carteira : MonoBehaviour
{
    private float saldo = 800f;
    public TextMeshProUGUI textoSaldo;

    public float Saldo => saldo;

    void Start()
    {
        AtualizarTextoSaldo();
    }

    public void Adicionar(float valor)
    {
        saldo += valor;
        AtualizarTextoSaldo();
    }

    public void Subtrair(float valor)
    {
        saldo -= valor;
        AtualizarTextoSaldo();
    }

    public bool PedirEmprestimo(float valor)
    {
        if (saldo < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void AtualizarTextoSaldo()
    {
        if (textoSaldo != null)
        {
            textoSaldo.text = "Saldo: " + saldo.ToString("F2");

            if (Saldo < 0)
            {
                textoSaldo.color = Color.red;
            }
            else
            {
                textoSaldo.color = Color.green;
            }
        }

        
    }

    public void receberemprestimo(float valor)
    {
        saldo += valor;
        AtualizarTextoSaldo();
    }
}
