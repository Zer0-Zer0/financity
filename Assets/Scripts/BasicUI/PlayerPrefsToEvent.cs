using UnityEngine;
using UnityEngine.Events;

public class PlayerPrefsToEvent : MonoBehaviour
{
    //Vai assumir que a pp é uma int
    [SerializeField]
    private string pref;

    [SerializeField]
    private UnityEvent prefChanged;

    [SerializeField]
    private int DesiredValue;

    void Start()
    {
        int _prefValue = PlayerPrefs.GetInt(pref, 0);
        if (_prefValue == DesiredValue)
        {
            prefChanged?.Invoke();
        }
    }

    public void SetPlayerPrefs(int value)
    {
        PlayerPrefs.SetInt(pref, value);
        PlayerPrefs.Save();
    }
}
