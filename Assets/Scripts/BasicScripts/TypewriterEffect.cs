using System.Collections;
using System;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_Text))]
public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float _normalDelay = 0.2f;
    private TMP_Text _textComponent;
    private string _fullText;
    private int _visibleCharacterCount = 0;
    public UnityEvent TypingFinished;
    private float _currentDelay;

    void Awake()
    {
        _textComponent = GetComponent<TMP_Text>();
        _fullText = _textComponent.text;
        _currentDelay = _normalDelay;
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
            _textComponent.maxVisibleCharacters = _visibleCharacterCount;
            _visibleCharacterCount++;

            yield return new WaitForSeconds(_currentDelay);
        }

        TypingFinished?.Invoke();
    }

    public void ClearText()
    {
        _visibleCharacterCount = 0;
    }
}