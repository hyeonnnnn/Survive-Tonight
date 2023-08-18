using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    public const int totalStage = 5;
    public int wholeStage = totalStage;
    public int[] spawnCount = new int[totalStage];
    public int currentStage = 1;

    [SerializeField] Timer timer;
    [SerializeField] DayAndNight dayAndNight;
    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] BossEnemy theBossEnemy;
    [SerializeField] GameObject go_Boss;
    private bool isStageIncreased = false;

    void Start()
    {
        StartCoroutine(CheckDayNight());
    }

    IEnumerator CheckDayNight()
    {
        while (true)
        {
            // ��ħ�̸�
            if (!dayAndNight.isNight && !isStageIncreased)
            {
                timer.RestartTimer();

                if (currentStage == totalStage)
                {
                    go_Boss.SetActive(true);
                    theBossEnemy.ShowBossInfo();
                }

                // timer.timerText.gameObject.SetActive(true); // Ÿ�̸� ����
                stageText.text = currentStage + " Stage";
                currentStage++;
                isStageIncreased = true;
            }

            // ���̸�
            else if (dayAndNight.isNight)
            {
                timer.RestartTimer();
                isStageIncreased = false;
            }

            // ���������� ������
            if (currentStage > totalStage)
            {
                SceneManager.LoadScene("GameOver");
                yield break; // �ڷ�ƾ ����
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}