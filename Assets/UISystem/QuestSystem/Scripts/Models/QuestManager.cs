//Fonte de inspiração
//https://www.youtube.com/watch?v=-65u991cdtw

using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using System;

namespace QuestSystem
{
    [Serializable]
    public class QuestManager
    {
        private QuestSO quest;
        public QuestSO Quest { get => quest; private set => quest = value; }

        private bool completed;
        public bool Completed { get => completed; private set => completed = value; }

        private List<GoalManager> goalManagers;
        public List<GoalManager> GoalManagers { get => goalManagers; private set => goalManagers = value; }

        [HideInInspector]
        public UnityEvent OnQuestCompletedEvent;

        public QuestManager(QuestSO questSO)
        {
            quest = questSO;
            foreach (var goal in quest.Goals)
                goalManagers.Add(new GoalManager(goal));
            OnQuestCompletedEvent = new UnityEvent();
        }

        public virtual void Initialize()
        {
            Completed = false;
            foreach (var goalManager in goalManagers)
            {
                goalManager.Initialize();
                goalManager.OnGoalCompletedEvent?.AddListener(CheckGoals);
            }
        }

        public virtual void UnInitialize()
        {
            foreach (var goalManager in goalManagers)
            {
                goalManager.OnGoalCompletedEvent?.RemoveListener(CheckGoals);
            }
        }

        private void CheckGoals()
        {
            Completed = goalManagers.All(g => g.Completed == true);

            if (Completed)
                OnQuestCompletedEvent?.Invoke();
        }
    }
}