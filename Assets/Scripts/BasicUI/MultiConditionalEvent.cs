using UnityEngine;
using UnityEngine.Events;

public class MultiConditionalEvent : MonoBehaviour
{
    [SerializeField]
    private int Conditions;

    [SerializeField]
    private UnityEvent ConditionsMet;

    public void OneConditionMet()
    {
        Conditions--;
        if (Conditions <= 0)
        {
            ConditionsMet?.Invoke();
        }
    }
}
