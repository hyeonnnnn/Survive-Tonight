using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] public float dayTime;
    [SerializeField] public float nightTime;
    public bool isNight;
    [SerializeField] TextMeshProUGUI DayAndNightText;

    void Start()
    {
        StartCoroutine(StartDay());
    }

    IEnumerator StartDay()
    {
        DayAndNightText.text = "Day";
        isNight = false;

        float startTime = Time.time;

        while (Time.time - startTime < dayTime)
        {
            float t = (Time.time - startTime) / dayTime;
            // float rotationAngle = Mathf.Lerp(5f, 175f, t);
            // transform.rotation = Quaternion.Euler(rotationAngle, 0f, 0f);

            yield return null;
        }

        StartCoroutine("StartNight");
    }

    IEnumerator StartNight()
    {
        isNight = true;
        DayAndNightText.text = "Night";

        yield return new WaitForSeconds(nightTime);

        StartCoroutine("StartDay");
    }
}
