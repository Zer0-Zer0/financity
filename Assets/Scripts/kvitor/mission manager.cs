using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    private List<Mission> missions = new List<Mission>();

    public void AddMission(Mission mission)
    {
        missions.Add(mission);
    }

    public void CompleteObjective(string missionTitle, int objectiveId)
    {
        Mission mission = missions.Find(m => m.Title == missionTitle);
        if (mission != null)
        {
            mission.CompleteObjective(objectiveId);
        }
    }

    public void DisplayMissions()
    {
        foreach (Mission mission in missions)
        {
            string status = mission.IsCompleted ? "Completed" : "In Progress";
            Debug.Log($"Mission: {mission.Title} - {status}");
            foreach (var obj in mission.Objectives)
            {
                string objStatus = obj.completed ? "Completed" : "Pending";
                Debug.Log($"  Objective: {obj.text} - {objStatus}");
            }
        }
    }
}
