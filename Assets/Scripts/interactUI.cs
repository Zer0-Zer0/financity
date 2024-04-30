using UnityEngine;

public class Interacao : MonoBehaviour
{
    public GameObject uiInteracao;
    public float distanciaMaxima = 2f;

    void FixedUpdate()
    {
        GameObject objetoInteracao = EncontrarObjetoInteracao();

        if (objetoInteracao != null)
        {
            Vector3 posicaoTela = Camera.main.WorldToScreenPoint(objetoInteracao.transform.position);

            uiInteracao.transform.position = posicaoTela + Vector3.up * 80f;
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
        Vector3 posicaoOrigem = transform.position;

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
