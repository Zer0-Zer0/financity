using UnityEngine;
using UISystem;

public class QuestSystemViewModel : MonoBehaviour
{
    [SerializeField] QuestSystemView questSystemView;
    [SerializeField] string initialObjective;

    void Start() => questSystemView.SetObjective(initialObjective);
    public void OnNewQuestAppeared(Component component, object data)
    {
        if (data is string objective)
            questSystemView.SetObjective(objective);
    }
}