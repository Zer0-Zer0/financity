using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChangeEmitter : MonoBehaviour
{
    [SerializeField] GameEvent OnQuestChanged;
    [SerializeField] string NewObjective;
    public void ChangeQuest(Component component, object data) => OnQuestChanged.Raise(this, NewObjective);
}
