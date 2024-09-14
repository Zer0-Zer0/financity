//Fonte de inspiração
//https://www.youtube.com/watch?v=-65u991cdtw

using System.Collections.Generic;
using System.Linq;

namespace QuestSystem
{
    public class QuestManager
    {
        public QuestSO quest { get; private set; }
        public bool Completed { get; private set; }
        public List<GoalManager> goalManagers { get; private set; }

        public virtual void Initialize()
        {
            Completed = false;
            foreach (var goalManager in goalManagers)
            {
                goalManager.Initialize();
                goalManager.goal.OnGoalCompletedEvent.AddListener(CheckGoals);
            }
        }

        private void CheckGoals()
        {
            Completed = goalManagers.All(g => g.Completed == true);

            if (Completed)
                quest.OnQuestCompletedEvent?.Invoke();
        }
    }
}