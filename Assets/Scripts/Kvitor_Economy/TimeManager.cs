using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;
using System;

public class TimeManager : MonoBehaviour
{
    public readonly int MINUTES_IN_A_DAY = 1440;
    readonly string[] WEEKDAYS = { "Domingo", "Segunda-Feira", "Terça-Feira", "Quarta-Feira", "Quinta-Feira", "Sexta-Feira", "Sábado" };
    [SerializeField] float _dayDuration = 60f;
    [SerializeField] int _initialTime = 0;
    [SerializeField] DateTime _initialDate = new DateTime(2024, 1, 1);

    [System.NonSerialized] public int time;
    [System.NonSerialized] public DateTime CurrentDate;

    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] TextMeshProUGUI timeText;

    public UnityEvent<DateTime> DayPassed;
    public UnityEvent<string> MinutePassed;

    void Start()
    {
        time = _initialTime;
        CurrentDate = _initialDate;

        //Assim, vai demorar um dia para um dia passar um "report"
        float _remainingDayDuration = _dayDuration - _initialTime;
        InvokeRepeating("OnDayPassed", _remainingDayDuration, _dayDuration);
        InvokeRepeating("OnMinutePassed", 0, _dayDuration / MINUTES_IN_A_DAY);
    }

    void OnMinutePassed()
    {
        time++;
        string formattedMinutes = FormatTime(time);
        MinutePassed?.Invoke(formattedMinutes);
    }

    void OnDayPassed()
    {
        time = 0;
        CurrentDate = CurrentDate.AddDays(1);
        UpdateDayText();
        DayPassed?.Invoke(CurrentDate);
    }

    string FormatTime(int totalMinutes)
    {
        int _hours = totalMinutes / 60;
        int _minutes = totalMinutes % 60;

        string FormattedText = string.Format("{0:00}:{1:00}", _hours, _minutes);

        return FormattedText;
    }

    void UpdateDayText()
    {
        string dayOfWeek = CurrentDate.ToString("dddd", new System.Globalization.CultureInfo("pt-BR"));
        string dateString = CurrentDate.ToString("dd/MM/yyyy");
        dayText.text = $"{dayOfWeek} - {dateString}";
    }
}