using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIWatch : MonoBehaviour
{
    private float startTime;  // 게임 시작 시간을 저장할 변수
    private float elapsedTime; // 경과 시간을 저장할 변수 

    [SerializeField] private TextMeshProUGUI timerText; // 플레이 화면상 보이는 경과 시간 컴포넌트
    [SerializeField] private TextMeshProUGUI liveTimeText; // 게임오버시 보여주는 시간 컴포넌트

    void Start()
    {
        elapsedTime = 0f;
        startTime = Time.time;
    }

    private void Update()
    {
        if (!GameManager.Instance.gameOver)
        {
            elapsedTime = Time.time - startTime; // 현재 시간에서 시작 시간을 빼서 경과 시간을 계산
            DisplayTime(elapsedTime); // 경과 시간을 표시
        }
        else
        {
            DisplayLiveTime();
        }
    }

    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); // 경과 시간을 분으로 변환
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); // 남은 시간을 초로 변환

        if (timerText != null)
            timerText.text = string.Format("생존시간 {0:00}:{1:00}", minutes, seconds); // "분:초" 형식으로 UI에 표시
    }

    public void DisplayLiveTime()
    {
        GameManager.Instance.gameOver = true;

        float minutes = Mathf.FloorToInt(elapsedTime / 60); // 경과 시간을 분으로 변환
        float seconds = Mathf.FloorToInt(elapsedTime % 60); // 남은 시간을 초로 변환

        if (liveTimeText != null)
            liveTimeText.text = string.Format("생존시간 {0:00}:{1:00}", minutes, seconds); // "분:초" 형식으로 UI에 표시
    }
}
