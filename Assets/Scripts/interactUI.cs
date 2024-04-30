using UnityEngine;

public class Interacao : MonoBehaviour
{
    public GameObject uiInteracao; // Referência ao UI de interação
    public float distanciaMaxima = 2f; // Distância máxima para exibir o UI de interação

    void FixedUpdate()
    {
        GameObject objetoInteracao = EncontrarObjetoInteracao();

        if (objetoInteracao != null)
        {
            // Obtém a posição do objeto de interação na tela
            Vector3 posicaoTela = Camera.main.WorldToScreenPoint(objetoInteracao.transform.position);

            // Ajusta a posição do UI de interação para ficar próxima ao objeto de interação
            uiInteracao.transform.position = posicaoTela + Vector3.up * 50f; // Ajuste conforme necessário
            uiInteracao.SetActive(true);
        }
        else
        {
            uiInteracao.SetActive(false);
        }
    }

    GameObject EncontrarObjetoInteracao()
    {
        RaycastHit hit;
        Vector3 posicaoOrigem = transform.position; // Posição do jogador

        // Lança um raio para frente a partir da posição do jogador
        if (Physics.Raycast(posicaoOrigem, transform.forward, out hit, distanciaMaxima))
        {
            if (hit.collider.CompareTag("Interact"))
            {
                return hit.collider.gameObject;
            }
        }

        return null;
    }
}
