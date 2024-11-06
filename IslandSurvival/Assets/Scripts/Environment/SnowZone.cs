using System.Collections;
using UnityEngine;

public class SnowZone : MonoBehaviour
{
    private float temperatureAmount = 1f; // �µ��� �󸶳� ���� ������ ����
    private Coroutine temperatureCoroutine; // �ڷ�ƾ �ν��Ͻ��� ������ ����

    PlayerCondition condition;
   

    private void Start()
    {
        condition = CharacterManager.Instance.Player.condition;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ĳ�������� Ȯ��
        if (temperatureCoroutine == null) // �ߺ� ���� ����
        {
            if(temperatureCoroutine != null)
                StopCoroutine(temperatureCoroutine);
            temperatureCoroutine = StartCoroutine(DecreaseTemperature());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ĳ���Ͱ� ������ ����� ��, �µ� ���Ҹ� �����մϴ�.
        if (other.CompareTag("Player"))
        {
            if (temperatureCoroutine != null) // �ڷ�ƾ�� ���� ���� ���
                StopCoroutine(temperatureCoroutine);
            temperatureCoroutine = StartCoroutine(IncreaseTemperature());
        }
    }

    private IEnumerator DecreaseTemperature()
    {
        while (true) // ���� ����
        {
            if (condition != null)
            {
                condition.DecreaseTemperature(temperatureAmount);
            }
            yield return new WaitForSeconds(1f); // 1�ʸ��� ����
        }
    }

    private IEnumerator IncreaseTemperature()
    {
        while (true) // ���� ����
        {
            if (condition != null && condition.temperature.curValue < 36f)
            {
                // �µ��� 36�� �̸��� ��쿡�� ����
                condition.IncreaseTemperature(temperatureAmount);
            }
            else
            {
                // �µ��� 36���� �Ǿ��� �� �ڷ�ƾ ����
                yield break;
            }
            yield return new WaitForSeconds(1f); // 1�ʸ��� ����
        }
    }
}
