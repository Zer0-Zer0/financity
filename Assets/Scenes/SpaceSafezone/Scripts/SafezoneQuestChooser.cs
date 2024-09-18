using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafezoneQuestChooser : MonoBehaviour
{
    [SerializeField] List<string> Quests = new List<string>();
    [SerializeField] GameEvent QuestEvent;
    void Start()
    {
        if (Quests.Count > 5)
        {
            Debug.LogError("Insufficient Quests");
            return;
        }

        if (DataManager.playerData.Days == 1 && DataManager.playerData.GetMissionOneStatus() == false)
        {
            QuestEvent.Raise(this, Quests[0]);
        }
        else if (DataManager.playerData.Days == 1 && DataManager.playerData.GetMissionOneStatus() == true)
        {
            QuestEvent.Raise(this, Quests[1]);
        }
        else if (DataManager.playerData.Days == 2)
        {
            QuestEvent.Raise(this, Quests[2]);
        }
        else if (DataManager.playerData.Days == 3)
        {
            QuestEvent.Raise(this, Quests[3]);
        }
        else
        {
            QuestEvent.Raise(this, Quests[4]);
        }
    }
}
