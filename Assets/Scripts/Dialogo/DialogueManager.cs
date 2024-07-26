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

    void BeginDialogue()
    {
        _dialogue.DialogueBegan?.Invoke(_dialogue);
        _dialogue.Conversation.ConversationBegan?.Invoke(_dialogue.Conversation);

        StartCoroutine(DisplayDialoguePhrases());

        _dialogue.Conversation.ConversationEnded?.Invoke();
        _dialogue.DialogueEnded?.Invoke();
    }

    IEnumerator DisplayDialoguePhrases()
    {
        foreach (DialoguePhrase phrase in _dialogue.Conversation.ConversationPhrases)
        {
            _typingDialogueCoroutine = StartCoroutine(DialogueTextbox.ShowText(phrase.Text));

            phrase.PhraseBegan?.Invoke(phrase);

            yield return _typingDialogueCoroutine; // Wait for text display to complete

            if (phrase.ConversationRoutes.Length != 0)
            {
                Debug.Log(phrase.ToString());
            }

            _waitForEPress = StartCoroutine(WaitForInput(KeyCode.E));
            yield return _waitForEPress; // Wait for input before proceeding

            phrase.PhraseEnded?.Invoke();
        }
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
        BeginDialogue();
    }
}