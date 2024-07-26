using System;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    public DialogueConversation Conversation;
}

[System.Serializable]
public struct DialogueConversation
{
    [SerializeField] private DialoguePhrase[] _conversationPhrases;
    public DialoguePhrase[] ConversationPhrases
    {
        get
        {
            return _conversationPhrases;
        }
        set
        {
            _conversationPhrases = value;
        }
    }

    public UnityEvent<DialogueConversation> ConversationBegan;
    public UnityEvent ConversationEnded;
}

[System.Serializable]
public struct DialogueConversationRoute
{
    [SerializeField] private string _text;
    public string Text
    {
        get { return _text; }
        private set { _text = value; }
    }

    [SerializeField] private DialogueConversation _route;
    public DialogueConversation Route
    {
        get
        {
            return _route;
        }
        set
        {
            _route = value;
        }
    }

    public UnityEvent RouteChosen;
}

[System.Serializable]
public struct DialoguePhrase
{
    [SerializeField] private DialogueConversationRoute[] _conversationRoutes;
    public DialogueConversationRoute[] ConversationRoutes
    {
        get
        {
            return _conversationRoutes;
        }
        set
        {
            _conversationRoutes = value;
        }
    }

    [SerializeField] private string _text;
    public string Text
    {
        get { return _text; }
        private set { _text = value; }
    }

    public UnityEvent<DialoguePhrase> PhraseBegan;
    public UnityEvent PhraseEnded;
}