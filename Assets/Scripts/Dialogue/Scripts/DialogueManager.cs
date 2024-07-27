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
            StopAllCoroutines();

            _dialogue = value;
            StartCoroutine(DisplayDialoguePhrases());
        }
    }

    [SerializeField] private OptionsManager _optionsManager;
    [SerializeField] private GameObject _optionsBox;
    [SerializeField] private GameObject _dialogueBox;

    [SerializeField] private TypewriterEffect DialogueTextbox;

    private Coroutine _typingDialogueCoroutine;
    private Coroutine _waitForEPress;

    private void Awake()
    {
        _optionsManager.AllListenersCleared.AddListener(OnAllListenersCleared);
        OnAllListenersCleared();
    }

    private void OnAllListenersCleared()
    {
        _optionsManager.AssignToEveryButton(OnOptionChosen);
    }

    public void EndDialogue()
    {
        CurrentDialogue.DialogueEnded?.Invoke();
        CurrentDialogue.Conversation.ConversationEnded?.Invoke(CurrentDialogue.Conversation);

        StopCoroutine(DisplayDialoguePhrases());
        _dialogueBox.SetActive(false);
    }

    public void OptionsPopup(DialoguePhrase dialoguePhrase)
    {
        _optionsBox.gameObject.SetActive(true);
        _optionsManager.OnOptionsPopup(dialoguePhrase);
    }

    public void OnOptionChosen()
    {
        _optionsBox.gameObject.SetActive(false);
    }

    public void BeginDialogue()
    {
        CurrentDialogue.DialogueBegan?.Invoke();
        CurrentDialogue.Conversation.ConversationBegan?.Invoke(CurrentDialogue.Conversation);

        StartCoroutine(DisplayDialoguePhrases());
        _dialogueBox.SetActive(true);
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

        CurrentDialogue.Conversation.ConversationEnded?.Invoke(CurrentDialogue.Conversation);
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