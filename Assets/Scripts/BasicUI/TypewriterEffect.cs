using System.Collections;
using System;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_Text))]
public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;
    private TMP_Text _textComponent;
    private string _fullText;
    private int _visibleCharacterCount = 0;
    public UnityEvent TypingFinished;

    void Awake()
    {
        _textComponent = GetComponent<TMP_Text>();
        _fullText = _textComponent.text;
    }

    void Start()
    {
        StartCoroutine(ShowText());
    }

    public IEnumerator ShowText(string text = "")
    {
        if (text == "")
        {
            text = _fullText;
        }
        else
        {
            _fullText = text;
            _textComponent.text = text;
        }

        while (_visibleCharacterCount <= text.Length)
        {
            _textComponent.maxVisibleCharacters = _visibleCharacterCount;
            _visibleCharacterCount++;

            yield return new WaitForSeconds(delay);
        }

        TypingFinished?.Invoke();
    }
}