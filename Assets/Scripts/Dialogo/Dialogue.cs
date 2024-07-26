using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    public DialogueConversation Conversation;

    public UnityEvent<Dialogue> DialogueBegan;
    public UnityEvent DialogueEnded;

    public override string ToString()
    {
        return $"Dialogue: {Conversation}";
    }
}

[System.Serializable]
public struct DialogueConversation
{
    [SerializeField] private DialoguePhrase[] _conversationPhrases;
    public DialoguePhrase[] ConversationPhrases
    {
        get { return _conversationPhrases; }
        set { _conversationPhrases = value; }
    }

    public UnityEvent<DialogueConversation> ConversationBegan;
    public UnityEvent ConversationEnded;

    public override string ToString()
    {
        return $"Conversation: {string.Join(", ", _conversationPhrases)}";
    }
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
        get { return _route; }
        set { _route = value; }
    }

    public UnityEvent RouteChosen;

    public override string ToString()
    {
        return $"Route text: {Text} \nRoute: {Route}";
    }
}

[System.Serializable]
public struct DialoguePhrase
{
    [SerializeField] private DialogueConversationRoute[] _conversationRoutes;
    public DialogueConversationRoute[] ConversationRoutes
    {
        get { return _conversationRoutes; }
        set { _conversationRoutes = value; }
    }

    [SerializeField] private string _text;
    public string Text
    {
        get { return _text; }
        private set { _text = value; }
    }

    public UnityEvent<DialoguePhrase> PhraseBegan;
    public UnityEvent PhraseEnded;

    public override string ToString()
    {
        return $"Phrase: {Text} \nConversationRoutes: {string.Join(", ", ConversationRoutes)}";
    }
}
