using UnityEngine;

public class InteracaoNPC : MonoBehaviour
{
    /*
    Para isto funcionar o player tem que está com a tag de player
    distancia tem que ser 10
    o colidder do npc tem que ser um trigger
    melhor distancia é x 5 y 5 z 5
    falta uma desativação depois que já foi ativo

    */


    public Dialogo dialogo;
    public float distanciaMaxima = 2f;
    public KeyCode teclaInteracao = KeyCode.E;
    public string[] mensagens;

    bool interacaoPossivel = false;

    void Update()
    {
        if (Input.GetKeyDown(teclaInteracao) && interacaoPossivel)
        {
            if (mensagens == null || mensagens.Length == 0)
            {
                mensagens = new string[] { "Mensagem padrão 1", "Mensagem padrão 2" };
            }

            dialogo.IniciarDialogo(mensagens);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interacaoPossivel = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interacaoPossivel = false;
        }
    }
}
