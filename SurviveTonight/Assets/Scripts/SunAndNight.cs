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
        float startTime = Time.time; // �¾� ȸ�� ���� �ð�

        while (Time.time - startTime < 600f) // 600�� ���ȸ� ȸ��
        {
            float t = (Time.time - startTime) / 600f; // ��� �ð��� ���� ����ȭ �� (0~1 ����)

            // 0������ 180���� ȸ���ϴ� ���� ������ ���
            float rotationAngle = Mathf.Lerp(0f, 180f, t);

            // �¾� ȸ��
            transform.rotation = Quaternion.Euler(rotationAngle, 0f, 0f);

            yield return null;
        }

        // 600�ʰ� ������ �¾� ȸ���� ����
    }
}
