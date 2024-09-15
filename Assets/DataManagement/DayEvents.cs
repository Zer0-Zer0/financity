using UnityEngine;
using UnityEngine.Events;

public class DayEvents : MonoBehaviour
{
    [SerializeField] GameEvent FirstDay;
    [SerializeField] GameEvent SecondDay;
    [SerializeField] GameEvent ThirdDay;
    private void Start()
    {
        switch (DataManager.playerData.Days)
        {
            case 1:
                FirstDay.Raise(this, DataManager.playerData.Days);
                break;
            case 2:
                SecondDay.Raise(this, DataManager.playerData.Days);
                break;
            case 3:
                ThirdDay.Raise(this, DataManager.playerData.Days);
                break;
        }
    }
}