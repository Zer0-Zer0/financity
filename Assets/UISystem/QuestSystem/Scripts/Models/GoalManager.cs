//Fonte de inspiração
//https://www.youtube.com/watch?v=-65u991cdtw

namespace QuestSystem
{
    public class GoalManager
    {
        public QuestGoalSO goal { get; private set; }
        public bool Completed { get; private set; }
        int CurrentGoalAmount;

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
            goal.OnGoalCompletedEvent?.Invoke();
        }

        public void Skip()
        {
            Complete();
        }
    }
}