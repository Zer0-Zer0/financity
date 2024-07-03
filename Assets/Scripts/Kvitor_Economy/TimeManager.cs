using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public struct ClockString
{//Eventos só funcionam com objetos, confia em mim
    public string time;

    public ClockString(string time)
    {
        this.time = time;
    }
}

public class TimeManager : MonoBehaviour
{
    public readonly int MINUTES_IN_A_DAY = 1440;
    private readonly string[] WEEKDAYS = { "Domingo", "Segunda-Feira", "Terça-Feira", "Quarta-Feira", "Quinta-Feira", "Sexta-Feira", "Sábado" };

    [SerializeField] private float dayDuration = 60f;
    [SerializeField] private int initialTime = 0;
    [SerializeField] private DateTime initialDate = new DateTime(2024, 1, 1);

    [System.NonSerialized] public int time;
    [System.NonSerialized] public DateTime currentDate;

    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI timeText;

    public UnityEvent<DateTime> DayPassed;
    public UnityEvent<ClockString> MinutePassed;

    private void Start()
    {
        time = initialTime;
        currentDate = initialDate;

        float remainingDayDuration = dayDuration - initialTime;
        InvokeRepeating("OnDayPassed", remainingDayDuration, dayDuration);
        InvokeRepeating("OnMinutePassed", 0, dayDuration / MINUTES_IN_A_DAY);
    }

    private void OnMinutePassed()
    {
        time++;
        ClockString formattedMinutes = new ClockString(FormatTime(time));
        MinutePassed?.Invoke(formattedMinutes);
    }

    private void OnDayPassed()
    {
        time = 0;
        currentDate = currentDate.AddDays(1);
        UpdateDayText();
        DayPassed?.Invoke(currentDate);
    }

    public static string FormatTime(int totalMinutes)
    {
        int hours = totalMinutes / 60;
        int minutes = totalMinutes % 60;

        string formattedText = string.Format("{0:00}:{1:00}", hours, minutes);

        return formattedText;
    }

    private void UpdateDayText()
    {
        string dayOfWeek = currentDate.ToString("dddd", new System.Globalization.CultureInfo("pt-BR"));
        string dateString = currentDate.ToString("dd/MM/yyyy");
        dayText.text = $"{dayOfWeek} - {dateString}";
    }
}
