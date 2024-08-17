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

    [SerializeField] KeyCode _teclaInteracao = KeyCode.E;

    [SerializeField] string nomeNPC;

    [SerializeField] UnityEvent DialogoIniciou;

    [TextArea]
    [SerializeField] string[] Mensagens;

    [SerializeField] UnityEvent DialogoTerminou;

    bool _interacaoPossivel = false;
    public static bool InteracaoOcorrendo = false;

    void Update()
    {
        if (Input.GetKeyDown(_teclaInteracao) && _interacaoPossivel && !InteracaoOcorrendo)
        {
            InteracaoOcorrendo = true;
            DialogoIniciou?.Invoke();
            ChecaMensagens();
            Dialogo.Instance.DialogoAcabou.AddListener(OnDialogoAcabou);
            Dialogo.Instance.InicializarDialogo(Mensagens, nomeNPC);
        }
    }

    void OnDialogoAcabou(){
        DialogoTerminou?.Invoke();
        Dialogo.Instance.DialogoAcabou.RemoveListener(OnDialogoAcabou);
    }

    void ChecaMensagens()
    {
        if (Mensagens == null || Mensagens.Length == 0)
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
