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
            // 아침이면
            if (!dayAndNight.isNight && !isStageIncreased)
            {
                timer.RestartTimer();

                if (currentStage == totalStage)
                {
                    go_Boss.SetActive(true);
                    theBossEnemy.ShowBossInfo();
                }

                // timer.timerText.gameObject.SetActive(true); // 타이머 시작
                stageText.text = currentStage + " Stage";
                currentStage++;
                isStageIncreased = true;
            }

            // 밤이면
            else if (dayAndNight.isNight)
            {
                timer.RestartTimer();
                isStageIncreased = false;
            }

            // 스테이지가 끝나면
            if (currentStage > totalStage)
            {
                SceneManager.LoadScene("GameOver");
                yield break; // 코루틴 종료
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}