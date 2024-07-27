using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Dialogo : MonoBehaviour
{
    [SerializeField] private float _delay = 0.2f;
    [SerializeField] private KeyCode _inputProximaFrase = KeyCode.Return;

    private TextMeshProUGUI _textComponent;
    private string[] linhas;
    private int index;
    private Coroutine typingCoroutine;
    public UnityEvent DialogoAcabou;

    private void Awake()
    {
        GameObject _textComponentGameObject = TagFinder.FindObjectWithTag("DialogueText");
        _textComponent = _textComponentGameObject.GetComponent<TextMeshProUGUI>();
    }

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
