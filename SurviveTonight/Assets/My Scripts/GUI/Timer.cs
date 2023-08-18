using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI DayAndNightText;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    [SerializeField] DayAndNight theDayAndNight;
    [SerializeField] Stage theStage;

    float currentTime;

    void OnEnable()
    {
        currentTime = theDayAndNight.time;
    }

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

     // 타이머로 표시
    void SetTimerText()
    {
        timerText.text = currentTime.ToString("0");
        CheckStage();
    }

    void CheckStage()
    {
        if (theStage.currentStage > theStage.wholeStage)
            return;
    }

    public void RestartTimer()
    {
        enabled = true;
    }
}
