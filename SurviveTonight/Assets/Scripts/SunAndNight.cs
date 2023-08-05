using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunAndNight : MonoBehaviour
{
    //bool isNight = false;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartDay());
    }

    IEnumerator StartDay()
    {
        float startTime = Time.time; // 태양 회전 시작 시간

        while (Time.time - startTime < 600f) // 600초 동안만 회전
        {
            float t = (Time.time - startTime) / 600f; // 경과 시간에 대한 정규화 값 (0~1 사이)

            // 0도에서 180도로 회전하는 보간 각도를 계산
            float rotationAngle = Mathf.Lerp(0f, 180f, t);

            // 태양 회전
            transform.rotation = Quaternion.Euler(rotationAngle, 0f, 0f);

            yield return null;
        }

        // 600초가 지나면 태양 회전을 멈춤
    }
}
