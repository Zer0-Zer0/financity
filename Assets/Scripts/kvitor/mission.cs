using System.Collections.Generic;

public class Mission
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public List<Objective> Objectives { get; private set; }

    public Mission(string title, string description, List<Objective> objectives)
    {
        Title = title;
        Description = description;
        IsCompleted = false;
        Objectives = objectives;
    }

    public void CompleteObjective(int objectiveId)
    {
        Objective objective = Objectives.Find(o => o.id == objectiveId);
        if (objective != null)
        {
            objective.Complete();
            if (Objectives.TrueForAll(o => o.completed))
            {
                IsCompleted = true;
            }
        }
    }
}

public class Objective
{
    public int id { get; private set; }
    public string text { get; private set; }
    public bool completed { get; private set; }

    public Objective(int _id, string _text)
    {
        id = _id;
        text = _text;
        completed = false;
    }

    public void Complete()
    {
        completed = true;
    }
}
