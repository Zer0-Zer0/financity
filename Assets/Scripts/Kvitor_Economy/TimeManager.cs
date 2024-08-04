using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{
    public static readonly int MINUTES_IN_A_DAY = 1440;

    [SerializeField] private int dayDuration = 60;
    [SerializeField] private int initialTime = 0;
    [SerializeField] private DateTime initialDate = new DateTime(2024, 1, 1);

    [System.NonSerialized] public int time;
    [System.NonSerialized] public DateTime currentDate;

    public UnityEvent<EventObject> OnDayPassedEvent;
    public UnityEvent<EventObject> OnMinutePassedEvent;

    private void Start()
    {
        time = initialTime;
        currentDate = initialDate;

        int remainingDayDuration = dayDuration * MINUTES_IN_A_DAY/(MINUTES_IN_A_DAY - initialTime);
        InvokeRepeating("OnDayPassed", (float)remainingDayDuration, (float)dayDuration);
        InvokeRepeating("OnMinutePassed", 0, (float)dayDuration / (float)MINUTES_IN_A_DAY);
    }

    private void OnMinutePassed()
    {
        time++;
        EventObject formattedMinutes = new EventObject();
        formattedMinutes.text = FormatTime(time);
        formattedMinutes.integer = time;
        OnMinutePassedEvent?.Invoke(formattedMinutes);
    }

    private void OnDayPassed()
    {
        time = 0;
        currentDate = currentDate.AddDays(1);
        EventObject formattedDate = new EventObject();
        formattedDate.text = FormatDateText();
        OnDayPassedEvent?.Invoke(formattedDate);
    }

    public static string FormatTime(int totalMinutes)
    {
        int hours = totalMinutes / 60;
        int minutes = totalMinutes % 60;

        string formattedText = string.Format("{0:00}:{1:00}", hours, minutes);

        return formattedText;
    }

    private string FormatDateText()
    {
        string dayOfWeek = currentDate.ToString("dddd", new System.Globalization.CultureInfo("pt-BR"));
        string dateString = currentDate.ToString("dd/MM/yyyy");
        string formattedDateText = $"{dayOfWeek} - {dateString}";
        return formattedDateText;
    }
}
