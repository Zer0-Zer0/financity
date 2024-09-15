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
        private QuestGoalSO goal;
        public QuestGoalSO Goal { get => goal; private set => goal = value; }

        private bool completed;
        public bool Completed { get => completed; private set => completed = value; }

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