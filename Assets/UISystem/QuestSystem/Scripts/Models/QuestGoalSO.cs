//Fonte de inspiração
//https://www.youtube.com/watch?v=-65u991cdtw

using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem
{
    public abstract class QuestGoalSO : ScriptableObject
    {
        protected string Description;
        public int RequiredAmount { get; protected set; }

        public UnityEvent OnGoalCompletedEvent;

        public virtual string GetDescription() => Description;
    }
}