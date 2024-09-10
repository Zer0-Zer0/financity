using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChangeEmitter : MonoBehaviour
{
    [SerializeField] GameEvent OnQuestChanged;
    [SerializeField] string NewObjective;
    void OnEnable() => VerifyUsage();

    public void ChangeQuest() => OnQuestChanged.Raise(this, NewObjective);

    private void VerifyUsage()
    {
        if (NewObjective == "")
            Debug.LogWarning($"{gameObject.name}: QuestChangeEmitter used in a uninteded manner, its objective is null, use GameEventEmitter in these cases instead");
    }
}
