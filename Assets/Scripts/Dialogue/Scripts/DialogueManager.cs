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
        _dialogue.DialogueBegan?.Invoke();
        _dialogue.Conversation.ConversationBegan?.Invoke(_dialogue.Conversation);

        StartCoroutine(DisplayDialoguePhrases());
    }

    IEnumerator DisplayDialoguePhrases()
    {
        foreach (DialoguePhrase phrase in _dialogue.Conversation.ConversationPhrases)
        {
            _typingDialogueCoroutine = StartCoroutine(DialogueTextbox.ShowText(phrase.Text));

            phrase.PhraseBegan?.Invoke(phrase);

            yield return _typingDialogueCoroutine;

            _waitForEPress = StartCoroutine(Waiters.InputWaiter(KeyCode.E));
            yield return _waitForEPress;

            phrase.PhraseEnded?.Invoke(phrase);

            if (phrase.Routes.Length != 0)
            {
                Debug.Log(phrase.ToString());
            }
        }

        _dialogue.Conversation.ConversationEnded?.Invoke();
        _dialogue.DialogueEnded?.Invoke();//This makes sense trust me
    }

    void Start()
    {
        BeginDialogue();
    }
}