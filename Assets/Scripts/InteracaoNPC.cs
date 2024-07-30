using System;
using UnityEngine;
using UnityEngine.Events;

public class InteracaoNPC : MonoBehaviour
{
    /*
    Para isto funcionar o player tem que está com a tag de "Player"
    Para isto funcionar a caixa de dialogo tem que está com a tag de "DialogueBox"
    o npc deve conter um colliger que eh trigger
    melhor distancia é x 5 y 5 z 5
    */

    [SerializeField]
    private KeyCode _teclaInteracao = KeyCode.E;

    [SerializeField]
    private string nomeNPC;

    [SerializeField]
    private UnityEvent DialogoIniciou;

    [SerializeField]
    private Dialogo.Frases[] _mensagens;
    private bool _interacaoPossivel = false;

    void Update()
    {
        if (Input.GetKeyDown(_teclaInteracao) && _interacaoPossivel)
        {
            DialogoIniciou?.Invoke();
            ChecaMensagens();
            Dialogo.Instance.InicializarDialogo(_mensagens, nomeNPC);
        }
    }

    void ChecaMensagens()
    {
        if (_mensagens == null || _mensagens.Length == 0)
        {
            throw new Exception("Nao ha frases para falar");
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
