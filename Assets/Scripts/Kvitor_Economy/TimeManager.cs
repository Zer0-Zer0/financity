using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;
using System;

public class TimeManager : MonoBehaviour
{
    readonly string[] WEEKDAYS = { "Domingo", "Segunda-Feira", "Terça-Feira", "Quarta-Feira", "Quinta-Feira", "Sexta-Feira", "Sábado" };
    [SerializeField] float _dayDuration = 60f;
    [SerializeField] float _initialTime = 0f;
    [SerializeField] DateTime _initialDate = new DateTime(2024, 1, 1);
    [System.NonSerialized] public float time;
    [System.NonSerialized] public DateTime CurrentDate;

    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timeText;

    public UnityEvent<DateTime> DayPassed;
    void Start()
    {
        time = _initialTime;
        CurrentDate = _initialDate;

        //Assim, vai demorar um dia para um dia passar um "report"
        float _remainingDayDuration = _dayDuration - _initialTime;
        InvokeRepeating("OnDayPassed", _remainingDayDuration, _dayDuration); 
        InvokeRepeating("OnSecondPassed", 0, 1);
    }

    void Update()
    {
        time += Time.deltaTime / _dayDuration;
    }

    void OnSecondPassed(){
        UpdateTimeText();
    }

    void OnDayPassed()
    {
        CurrentDate.AddDays(1);
        UpdateDayText();
        DayPassed?.Invoke(CurrentDate);
    }

    void UpdateTimeText()
    {
        int hours = Mathf.FloorToInt(time * 24);
        int minutes = Mathf.FloorToInt((time * 24 * 60) % 60);
        string timeString = string.Format("{0:00}:{1:00}", hours, minutes);
        timeText.text = timeString;
    }

    void UpdateDayText()
    {
        string dayOfWeek = CurrentDate.ToString("dddd", new System.Globalization.CultureInfo("pt-BR"));
        string dateString = CurrentDate.ToString("dd/MM/yyyy");
        dayText.text = $"{dayOfWeek} - {dateString}";
    }
}