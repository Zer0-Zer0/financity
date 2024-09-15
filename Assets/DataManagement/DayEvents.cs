using UnityEngine;
using UnityEngine.Events;

public class DayEvents : MonoBehaviour
{
    [SerializeField] GameEvent FirstDay;
    [SerializeField] GameEvent SecondDay;
    [SerializeField] GameEvent ThirdDay;
    private void Start()
    {
        Debug.Log("Quantia de dias: {DataManager.playerData.Days}");
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

    public void AddDay(){
        DataManager.playerData.Days++;
    }
}