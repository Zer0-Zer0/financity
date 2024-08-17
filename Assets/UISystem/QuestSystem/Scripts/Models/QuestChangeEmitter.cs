using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChangeEmitter : MonoBehaviour
{
    [SerializeField] GameEvent OnQuestChanged;
    [SerializeField] string NewObjective;
    public void ChangeQuest() => OnQuestChanged.Raise(this, NewObjective);
}
