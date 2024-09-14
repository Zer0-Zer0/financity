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

        public QuestManager(QuestSO questSO)
        {
            quest = questSO;
            foreach (var goal in quest.Goals)
                goalManagers.Add(new GoalManager(goal));
        }

        public virtual void Initialize()
        {
            Completed = false;
            foreach (var goalManager in goalManagers)
            {
                goalManager.Initialize();
                goalManager.goal.OnGoalCompletedEvent.AddListener(CheckGoals);
            }
        }

        public virtual void UnInitialize()
        {
            foreach (var goalManager in goalManagers)
            {
                goalManager.goal.OnGoalCompletedEvent.RemoveListener(CheckGoals);
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