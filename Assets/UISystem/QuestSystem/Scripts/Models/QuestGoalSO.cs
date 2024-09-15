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
        private int requiredAmount;
        public int RequiredAmount { get => requiredAmount; private set => requiredAmount = value; }

        public virtual string GetDescription() => Description;
    }
}