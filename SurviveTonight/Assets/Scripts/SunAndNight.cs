using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunAndNight : MonoBehaviour
{
    int DayTime = 30;
    bool isNight;

    void Update()
    {
        StartCoroutine(StartDay());
    }

    IEnumerator StartDay()
    {
        isNight = false;

        float startTime = Time.time;

        while (Time.time - startTime < DayTime)
        {
            float t = (Time.time - startTime) / DayTime;
            float rotationAngle = Mathf.Lerp(5f, 175f, t);
            transform.rotation = Quaternion.Euler(rotationAngle, 0f, 0f);

            yield return null;
        }
        StartCoroutine("StartNight");
    }

    IEnumerator StartNight()
    {
        isNight = true;

        while (isNight)
        {
            transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        }

        StartCoroutine("StartDay");
        yield return null;
    }
}
