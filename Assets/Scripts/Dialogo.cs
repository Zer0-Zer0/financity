using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(TypewriterEffect))]
public class Dialogo : MonoBehaviour
{
    /*
        Para funcionar:
        Adicione isso a caixa de texto na qual o dialogo sera exibido
        SE HOUVER MAIS DE UM SCRIPT DESSE EM UMA CENA, UM ERRO SERA OUTPUTEADO
    */
    [SerializeField] private KeyCode _inputProximaFrase = KeyCode.Return;
    private int index;
    private string[] linhas;
    private Coroutine typingCoroutine;
    public UnityEvent DialogoAcabou;
    private TypewriterEffect _textbox;
    
    public static Dialogo Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            throw new Exception("ERROR: Atempted to add a second instance to the Dialogo singleton");
        }
        else
        {
            Instance = this;
        }

        _textbox = GetComponent<TypewriterEffect>();
    }

    public void InicializarDialogo(string[] linhasDialogo)
    {
        linhas = linhasDialogo;
        index = 0;

        typingCoroutine = StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine()
    {
        yield return _textbox.ShowText(linhas[index]);
        yield return Waiters.InputWaiter(_inputProximaFrase);
        ProximaFrase();
    }

    void ProximaFrase()
    {
        if (index < linhas.Length - 1)
        {
            index++;
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            _textbox.ClearText();
            DialogoAcabou?.Invoke();
        }
    }
}
