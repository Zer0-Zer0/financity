using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private TypewriterEffect DialogueTextbox;

    IEnumerator BeginDialogue()
    {
        foreach (DialoguePhrase phrase in _dialogue.Conversation.ConversationPhrases)
        {
            StartCoroutine(DialogueTextbox.ShowText(phrase.Text));
            if (phrase.ConversationRoutes != null)
            {
                Debug.Log(phrase.ConversationRoutes);
            }
            StartCoroutine(Waiters.InputWaiter(KeyCode.E));
        }

        yield return null;
    }
}