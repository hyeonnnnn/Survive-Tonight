using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    int dayTime = 600;
    bool isNight;

    void Update()
    {
        StartCoroutine(StartDay());
    }

    IEnumerator StartDay()
    {
        isNight = false;

        float startTime = Time.time;

        while (Time.time - startTime < dayTime)
        {
            float t = (Time.time - startTime) / dayTime;
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
