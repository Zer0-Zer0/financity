//Fonte de inspiração
//https://www.youtube.com/watch?v=-65u991cdtw

using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem
{
    [CreateAssetMenu(fileName = "GoalGather_", menuName = "ScriptableObjects/Goals/Gather")]
    public class GatherGoalSO : QuestGoalSO
    {
        public string GatheredObject;
    }
}