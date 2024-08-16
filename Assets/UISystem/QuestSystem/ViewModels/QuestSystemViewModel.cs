using UnityEngine;
using UISystem;

public class QuestSystemViewModel : MonoBehaviour
{
    [SerializeField] QuestSystemView questSystemView;
    void OnNewQuestAppeared(Component component, object data){
        if (data is string objective)
            questSystemView.SetObjective(objective);
    }
}