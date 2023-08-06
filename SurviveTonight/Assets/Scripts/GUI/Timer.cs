using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI DayAndNightText;

    [Header("Timer Settings")]
    public float currentTime;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    // Update is called once per frame
    void Update()
    {
        CountDown();
    }

    void CountDown()
    {
        currentTime -= Time.deltaTime;

        if (hasLimit && ( currentTime <= timerLimit))
        {
            currentTime = timerLimit;
            SetTimerText();
            enabled = false;
        }

        SetTimerText();
    }

    void SetTimerText()
    {
        timerText.text = currentTime.ToString("0");
    }
}
