using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Dialogo : MonoBehaviour
{
/*
    Para funcionar:
    Adicione isso a caixa de texto na qual o dialogo sera exibido
    SE HOUVER MAIS DE UM SCRIPT DESSE EM UMA CENA, UM ERRO SERA OUTPUTEADO
*/

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

        _textComponent = GetComponent<TextMeshProUGUI>();
    }

    [SerializeField] private float _delay = 0.2f;
    [SerializeField] private KeyCode _inputProximaFrase = KeyCode.Return;

    private TextMeshProUGUI _textComponent;
    private string[] linhas;
    private int index;
    private Coroutine typingCoroutine;
    public UnityEvent DialogoAcabou;

    public void InicializarDialogo(string[] linhasDialogo)
    {
        linhas = linhasDialogo;
        index = 0;
        _textComponent.text = string.Empty;

        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        int _visibleCharacterCount = 0;

        while (_visibleCharacterCount <= linhas[index].Length)
        {
            _textComponent.maxVisibleCharacters = _visibleCharacterCount;
            _visibleCharacterCount++;

            yield return new WaitForSeconds(_delay);
        }

        yield return Waiters.InputWaiter(_inputProximaFrase);
        ProximaFrase();
    }

    void ProximaFrase()
    {
        if (index < linhas.Length - 1)
        {
            index++;
            _textComponent.text = string.Empty;
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            LimparTexto();
            DialogoAcabou?.Invoke();
        }
    }

    void LimparTexto() { _textComponent.text = string.Empty; }
}
