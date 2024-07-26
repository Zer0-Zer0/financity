using System;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
}

[System.Serializable]
public struct Conversation
{
    public static UnityEvent<Conversation> ConversationBegan;
    public static UnityEvent ConversationEnded;

    public Phrase[] Phrases;
}

[System.Serializable]
public struct ConversationRoute
{
    public string Text;
    public Conversation Route;
    public static UnityEvent RouteChosen;
}

[System.Serializable]
public struct Phrase
{
    public ConversationRoute[] ConversationRoutes;

    [SerializeField] private string _text;
    public string Text
    {
        get { return _text; }
        private set { _text = value; }
    }

    public UnityEvent<Phrase> PhraseBegan;
    public UnityEvent PhraseEnded;
}