using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] public float time;
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

        while (Time.time - startTime < time)
        {
            float t = (Time.time - startTime) / time;
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

        yield return new WaitForSeconds(time);

        StartCoroutine("StartDay");
    }
}
