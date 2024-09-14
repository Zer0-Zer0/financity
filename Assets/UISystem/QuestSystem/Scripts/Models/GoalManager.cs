//Fonte de inspiração
//https://www.youtube.com/watch?v=-65u991cdtw

using System;
using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem
{
    [Serializable]
    public class GoalManager
    {
        public QuestGoalSO goal { get; private set; }
        public bool Completed { get; private set; }
        int CurrentGoalAmount;
        [HideInInspector]
        public UnityEvent OnGoalCompletedEvent;

        public GoalManager(QuestGoalSO goalSO)
        {
            goal = goalSO;
            Completed = false;
            CurrentGoalAmount = 0;
            OnGoalCompletedEvent = new UnityEvent();
        }

        public virtual void Initialize()
        {
            Completed = false;
        }

        protected void Evaluate()
        {
            if (CurrentGoalAmount >= goal.RequiredAmount)
                Complete();
        }

        private void Complete()
        {
            Completed = true;
            OnGoalCompletedEvent?.Invoke();
        }

        public void Skip()
        {
            Complete();
        }
    }
}