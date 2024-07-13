using UnityEngine;

public class Interacao : MonoBehaviour
{
    public GameObject uiInteracao;
    public float raioDetecao = 2f;

    public float altura = 180f;
    public float distancia = 50f;

    private GameObject objetoInteracao;

    void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, raioDetecao);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Interact"))
            {
                objetoInteracao = collider.gameObject;
                MostrarUIInteracao();
                return;
            }
        }

        objetoInteracao = null;
        OcultarUIInteracao();
    }

    void MostrarUIInteracao()
    {
        if (objetoInteracao == null || uiInteracao == null)
            return;

        // Obtém o centro do colisor do objeto de interação
        Vector3 centroColisor = objetoInteracao.GetComponent<Collider>().bounds.center;

        // Converte o centro do colisor para a posição na tela
        Vector3 posicaoTela = Camera.main.WorldToScreenPoint(centroColisor);

        // Define a posição da UI de interação na tela
        uiInteracao.transform.position = posicaoTela + Vector3.up * altura + Vector3.back * distancia;

        // Ativa a UI de interação
        uiInteracao.SetActive(true);
    }

    void OcultarUIInteracao()
    {
        if (uiInteracao != null)
            uiInteracao.SetActive(false);
    }
}
