//Fonte de inspiração
//https://www.youtube.com/watch?v=-65u991cdtw

using UnityEngine;
using System;

namespace QuestSystem
{
    [Serializable]
    public abstract class QuestGoalSO : ScriptableObject
    {
        [SerializeField]
        protected string Description;
        [SerializeField]
        public int RequiredAmount { get; protected set; }

        public virtual string GetDescription() => Description;
    }
}