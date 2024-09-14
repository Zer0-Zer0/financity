//Fonte de inspiração
//https://www.youtube.com/watch?v=-65u991cdtw

using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace QuestSystem
{
    [CreateAssetMenu(fileName = "Quest_", menuName = "ScriptableObjects/Quest")]
    public class QuestSO : ScriptableObject
    {
        [Serializable]
        public struct Info
        {
            public string Name;
            public string Description;
        }

        [Header("Info")] public Info Information;

        public struct Stat
        {
            public float Value;
        }

        [Header("Reward")] public Stat Reward;

        public List<QuestGoalSO> Goals;

        public UnityEvent OnQuestCompletedEvent;
    }
}