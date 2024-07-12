using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ObjectiveSystem : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;
    private List<Objective> objectives = new List<Objective>();

    public AnimatedText text;

    public struct Objective
    {
        public int id;
        public string text;
        public bool completed;

        public Objective(int _id, string _text)
        {
            id = _id;
            text = _text;
            completed = false;
        }
    }

    public void AddObjective(int id, string objective)
    {
        objectives.Add(new Objective(id, objective));
        UpdateObjectiveText();
    }

    private void UpdateObjectiveText()
    {
        string objectiveString = "";
        foreach (Objective obj in objectives)
        {
            objectiveString += "- " + obj.text;
            if (obj.completed)
            {
                objectiveString += " [X]";
            }
            else
            {
                objectiveString += " [ ]";
            }
            objectiveString += "\n";
        }
        objectiveText.text = objectiveString;
    }

    public void CompleteObjective(int id)
    {
        int index = objectives.FindIndex(x => x.id == id);
        if (index != -1 && !objectives[index].completed)
        {
            objectives[index] = new Objective(objectives[index].id, objectives[index].text) { completed = true };
            UpdateObjectiveText();
            StartCoroutine(RemoveCompletedObjective(id));
            text.ShowText("Objetivo concluído!");
        }
    }

    IEnumerator RemoveCompletedObjective(int id)
    {
        yield return new WaitForSeconds(3f);
        objectives.RemoveAll(x => x.id == id && x.completed);
        UpdateObjectiveText();
    }
}
