using System.Collections;
using System;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
using Unity.VisualScripting;

[RequireComponent(typeof(TMP_Text))]
public class TypewriterEffect : MonoBehaviour
{
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

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            StopAllCoroutines();
            _textComponent.maxVisibleCharacters = _fullText.Length;
        }
    }

    public IEnumerator ShowText(string text = "")
    {
        ClearText();

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
            if (Input.GetKey(KeyCode.F))
            {
                _textComponent.maxVisibleCharacters = _fullText.Length;
                _textComponent.text = text;
                yield break;
            }

            _textComponent.maxVisibleCharacters = _visibleCharacterCount;
            _visibleCharacterCount++;

            yield return null;
        }

        TypingFinished?.Invoke();
    }

    public void ClearText()
    {
        _visibleCharacterCount = 0;
    }
}