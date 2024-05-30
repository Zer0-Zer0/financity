using UnityEngine;

public class Interacao : MonoBehaviour
{
    public GameObject uiInteracao;
    public float raioDetecao = 2f;

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
        Vector3 posicaoTela = Camera.main.WorldToScreenPoint(objetoInteracao.transform.position);
        uiInteracao.transform.position = posicaoTela + Vector3.up * 80f;
        uiInteracao.SetActive(true);
    }

    void OcultarUIInteracao()
    {
        uiInteracao.SetActive(false);
    }
}
