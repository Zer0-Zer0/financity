[System.Serializable]
public class PlayerData
{
    [SerializeField]
    int _currentAmmo = 30;

    [SerializeField]
    int _totalAmmo = 120;

    [SerializeField]
    float _currentBalance = 1440f;

    [SerializeField]
    bool _firstTime = true;

    [SerializeField]
    bool _missionOneCompleted = false;

    public UnityEvent CurrentAmmoChanged;
    public UnityEvent TotalAmmoChanged;
    public UnityEvent CurrentBalanceChanged;
    public UnityEvent FirstTimeChanged;
    public UnityEvent MissionOneCompleted;

    // Getter and Setter for CurrentAmmo
    public int GetCurrentAmmo()
    {
        return _currentAmmo;
    }

    public void SetCurrentAmmo(int value)
    {
        _currentAmmo = value;
        CurrentAmmoChanged?.Invoke();
    }

    // Getter and Setter for TotalAmmo
    public int GetTotalAmmo()
    {
        return _totalAmmo;
    }

    public void SetTotalAmmo(int value)
    {
        _totalAmmo = value;
        TotalAmmoChanged?.Invoke();
    }

    // Getter and Setter for CurrentBalance
    public float GetCurrentBalance()
    {
        return _currentBalance;
    }

    public void SetCurrentBalance(float value)
    {
        _currentBalance = value;
        CurrentBalanceChanged?.Invoke();
    }

    // Getter and Setter for FirstTime
    public bool GetFirstTime()
    {
        return _firstTime;
    }

    public void SetFirstTime(bool value)
    {
        _firstTime = value;
        FirstTimeChanged?.Invoke();
    }

    public bool GetMissionOneStatus()
    {
        return _missionOneCompleted;
    }

    public void CompleteMissionOne()
    {
        _missionOneCompleted = true;
        MissionOneCompleted?.Invoke();
    }

    public override string ToString()
    {
        return $"Current Ammo: {GetCurrentAmmo()}, Total Ammo: {GetTotalAmmo()}, Current Balance: {GetCurrentBalance()}, First Time: {GetFirstTime()}";
    }
}