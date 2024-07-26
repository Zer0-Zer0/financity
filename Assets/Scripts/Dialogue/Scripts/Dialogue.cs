using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    private string _name;
    public string Name
    {
        get { return _name; }
        private set { _name = value; }
    }
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
        return $"Conversation: {string.Join(", ", _conversationPhrases.Select(p => p.Text))}";
    }
}

[System.Serializable]
public class DialogueRoute
{
    [SerializeField] private string _text;
    public string Text
    {
        get { return _text; }
        private set { _text = value; }
    }

    public UnityEvent RouteChosen;

    public override string ToString()
    {
        return $"Route text: {Text}";
    }
}

[System.Serializable]
public class DialoguePhrase
{
    [SerializeField] private DialogueRoute[] _routes;
    public DialogueRoute[] Routes
    {
        get { return _routes; }
        set { _routes = value; }
    }
    [TextArea][SerializeField] private string _text;
    public string Text
    {
        get { return _text; }
        private set { _text = value; }
    }

    public UnityEvent<DialoguePhrase> PhraseBegan;
    public UnityEvent<DialoguePhrase> PhraseEnded;

    public override string ToString()
    {
        return $"Phrase: {Text} \nConversationRoutes: {string.Join(", ", Routes.Select(r => r.Text))}";
    }
}
