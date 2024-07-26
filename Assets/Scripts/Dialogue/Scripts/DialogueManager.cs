using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;
    public Dialogue CurrentDialogue
    {
        get
        {
            return _dialogue;
        }
        set
        {
            EndDialogue();
            StopAllCoroutines();

            _dialogue = value;
            StartCoroutine(DisplayDialoguePhrases());
        }
    }

    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private GameObject DialogueBox;

    [SerializeField] private TypewriterEffect DialogueTextbox;

    private Coroutine _typingDialogueCoroutine;
    private Coroutine _waitForEPress;

    void BeginDialogue()
    {
        CurrentDialogue.DialogueBegan?.Invoke();
        CurrentDialogue.Conversation.ConversationBegan?.Invoke(CurrentDialogue.Conversation);

        StartCoroutine(DisplayDialoguePhrases());
    }

    IEnumerator DisplayDialoguePhrases()
    {
        foreach (DialoguePhrase phrase in CurrentDialogue.Conversation.ConversationPhrases)
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

        EndDialogue();
    }

    public void EndDialogue(){
        CurrentDialogue.Conversation.ConversationEnded?.Invoke();
        CurrentDialogue.DialogueEnded?.Invoke();
    }

    public void SetDialogue(DialogueFile value)
    {
        CurrentDialogue = value.dialogue;
    }

    void Start()
    {
        BeginDialogue();
    }
}