using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Dialogo))]
public class InteracaoNPC : MonoBehaviour
{
    /*
    Para isto funcionar o player tem que está com a tag de "Player"
    Para isto funcionar a caixa de dialogo tem que está com a tag de "DialogueBox"
    o npc deve conter um colliger que eh trigger
    melhor distancia é x 5 y 5 z 5
    */

    [SerializeField] private KeyCode _teclaInteracao = KeyCode.E;
    [SerializeField] private string[] _mensagens;
    private Dialogo _dialogo;
    private bool _interacaoPossivel = false;

    private void Awake()
    {
        _dialogo = GetComponent<Dialogo>();
    }

    void Update()
    {
        if (Input.GetKeyDown(_teclaInteracao) && _interacaoPossivel)
        {
            ChecaMensagens();
            _dialogo.InicializarDialogo(_mensagens);
        }
    }

    void ChecaMensagens()
    {
        if (_mensagens == null || _mensagens.Length == 0)
        {
            _mensagens = new string[] { "Mensagem padrão 1", "Mensagem padrão 2" };
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _interacaoPossivel = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _interacaoPossivel = false;
        }
    }
}
