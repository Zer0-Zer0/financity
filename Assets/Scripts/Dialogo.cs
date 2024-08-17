using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Dialogo : MonoBehaviour
{
    /*
        Instruções para uso:
        - Este script deve ser único na cena. Se houver mais de uma instância, um aviso será exibido no console.
    */

    [SerializeField] KeyCode _inputProximaFrase = KeyCode.Return; // Tecla para avançar para a próxima frase
    public static Dialogo Instance { get; private set; } // Instância única do Dialogo

    private int index; // Índice da frase atual

    private string[] frases; // Array de frases
    private Coroutine typingCoroutine; // Coroutine para digitar a frase

    [SerializeField] TypewriterEffect _textoDialogo; // Efeito de digitação
    [SerializeField] TMP_Text _textoNome; // Texto do nome do falante
    [SerializeField] GameObject _caixaDialogo; // Caixa de diálogo

    public UnityEvent DialogoAcabou; // Evento a ser chamado quando o diálogo termina

    private void Awake()
    {
        // Verifica se já existe uma instância do Dialogo
        if (Instance != null && Instance != this)
            Debug.LogWarning("Mais de um objeto dialogo na cena");
        else
            Instance = this;

        _caixaDialogo.SetActive(false); // Desativa a caixa de diálogo inicialmente
    }

    public void InicializarDialogo(string[] frasesDialogo, string nomeFalante = "")
    {
        // Inicializa o diálogo com as frases e o nome do falante
        frases = frasesDialogo;
        index = 0;
        _textoNome.text = nomeFalante;
        _caixaDialogo.SetActive(true); // Ativa a caixa de diálogo
        typingCoroutine = StartCoroutine(TypeLine()); // Inicia a digitação da primeira frase
    }

    public IEnumerator TypeLine()
    {
        // Mostra a frase atual com efeito de digitação
        yield return _textoDialogo.ShowText(frases[index]);
        yield return Waiters.InputWaiter(_inputProximaFrase); // Espera pela entrada do usuário
        ProximaFrase(); // Avança para a próxima frase
    }

    void ProximaFrase()
    {
        // Verifica se há mais frases para mostrar
        if (index < frases.Length - 1)
        {
            index++; // Incrementa o índice
            typingCoroutine = StartCoroutine(TypeLine()); // Inicia a digitação da próxima frase
        }
        else
        {
            // Se não houver mais frases, limpa o texto e desativa a caixa de diálogo
            _textoDialogo.ClearText();
            _caixaDialogo.SetActive(false);
            InteracaoNPC.InteracaoOcorrendo = false; // Finaliza a interação com o NPC
            DialogoAcabou?.Invoke(); // Invoca o evento de diálogo acabado
        }
    }
}
