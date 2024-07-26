using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private TypewriterEffect DialogueTextbox;

    private Coroutine _typingDialogueCoroutine;
    private Coroutine _waitForEPress;

    IEnumerator BeginDialogue()
    {
        _dialogue.DialogueBegan?.Invoke(_dialogue);
        _dialogue.Conversation.ConversationBegan?.Invoke(_dialogue.Conversation);

        foreach (DialoguePhrase phrase in _dialogue.Conversation.ConversationPhrases)
        {
            phrase.PhraseBegan?.Invoke(phrase);
            _typingDialogueCoroutine = StartCoroutine(ShowDialogueText(phrase.Text));
            yield return _typingDialogueCoroutine; // Wait for text display to complete

            if (phrase.ConversationRoutes.Length != 0)
            {
                Debug.Log(phrase.ConversationRoutes.ToString);
            }

            _waitForEPress = StartCoroutine(WaitForInput(KeyCode.E));
            yield return _waitForEPress; // Wait for input before proceeding

            phrase.PhraseEnded?.Invoke();
        }

        _dialogue.Conversation.ConversationEnded?.Invoke();
        _dialogue.DialogueEnded?.Invoke();
    }

    IEnumerator ShowDialogueText(string text)
    {
        yield return DialogueTextbox.ShowText(text);
    }

    IEnumerator WaitForInput(KeyCode key)
    {
        while (!Input.GetKeyDown(key))
        {
            yield return null;
        }
    }

    void Start()
    {
        StartCoroutine(BeginDialogue());
    }
}