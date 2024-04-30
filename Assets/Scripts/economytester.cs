using UnityEngine;

public class TesteMovimentacaoCarteira : MonoBehaviour
{
    public Carteira carteira;

    void Start()
    {
        // Adiciona 100 à carteira
        carteira.Adicionar(100f);
        Debug.Log("Saldo atual: " + carteira.Saldo);

        // Subtrai 50 da carteira
        carteira.Subtrair(120f);
        Debug.Log("Saldo atual: " + carteira.Saldo);

        // Tenta pedir empréstimo de 200
        if (carteira.PedirEmprestimo(200f))
        {
            Debug.Log("Empréstimo concedido. Saldo atual: " + carteira.Saldo);
        }
        else
        {
            Debug.Log("Não há necessidade de empréstimo. Saldo atual: " + carteira.Saldo);
        }
    }
}
