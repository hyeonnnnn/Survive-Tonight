using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage : MonoBehaviour
{
    public const int totalStage = 5;
    public int wholeStage = totalStage;
    public int[] spawnCount = new int[totalStage];
    public int currentStage = 1;

    [SerializeField] Timer timer;
    [SerializeField] DayAndNight dayAndNight;
    [SerializeField] TextMeshProUGUI stageText;
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
                timer.timerText.gameObject.SetActive(true); // 타이머 시작
                stageText.text = currentStage + " Stage";
                currentStage++;
                isStageIncreased = true;
            }

            // 밤이면
            else if (dayAndNight.isNight)
            {
                isStageIncreased = false;
            }

            // 스테이지가 끝나면
            if (currentStage > totalStage)
            {
                Debug.Log("게임 승리");
                yield break; // 코루틴 종료
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}