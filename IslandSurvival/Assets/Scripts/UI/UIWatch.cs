using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIWatch : MonoBehaviour
{
    private float startTime;  // ���� ���� �ð��� ������ ����
    private float elapsedTime; // ��� �ð��� ������ ���� 

    [SerializeField] private TextMeshProUGUI timerText; // �÷��� ȭ��� ���̴� ��� �ð� ������Ʈ
    [SerializeField] private TextMeshProUGUI liveTimeText; // ���ӿ����� �����ִ� �ð� ������Ʈ

    void Start()
    {
        elapsedTime = 0f;
        startTime = Time.time;
    }

    private void Update()
    {
        if (!GameManager.Instance.gameOver)
        {
            elapsedTime = Time.time - startTime; // ���� �ð����� ���� �ð��� ���� ��� �ð��� ���
            DisplayTime(elapsedTime); // ��� �ð��� ǥ��
        }
        else
        {
            DisplayLiveTime();
        }
    }

    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); // ��� �ð��� ������ ��ȯ
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); // ���� �ð��� �ʷ� ��ȯ

        if (timerText != null)
            timerText.text = string.Format("�����ð� {0:00}:{1:00}", minutes, seconds); // "��:��" �������� UI�� ǥ��
    }

    public void DisplayLiveTime()
    {
        GameManager.Instance.gameOver = true;

        float minutes = Mathf.FloorToInt(elapsedTime / 60); // ��� �ð��� ������ ��ȯ
        float seconds = Mathf.FloorToInt(elapsedTime % 60); // ���� �ð��� �ʷ� ��ȯ

        if (liveTimeText != null)
            liveTimeText.text = string.Format("�����ð� {0:00}:{1:00}", minutes, seconds); // "��:��" �������� UI�� ǥ��
    }
}
