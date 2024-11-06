using System.Collections;
using UnityEngine;

public class SnowZone : MonoBehaviour
{
    private float temperatureAmount = 1f; // 온도를 얼마나 줄일 것인지 설정
    private Coroutine temperatureCoroutine; // 코루틴 인스턴스를 저장할 변수

    PlayerCondition condition;
   

    private void Start()
    {
        condition = CharacterManager.Instance.Player.condition;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 캐릭터인지 확인
        if (temperatureCoroutine == null) // 중복 실행 방지
        {
            if(temperatureCoroutine != null)
                StopCoroutine(temperatureCoroutine);
            temperatureCoroutine = StartCoroutine(DecreaseTemperature());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 캐릭터가 영역을 벗어났을 때, 온도 감소를 중지합니다.
        if (other.CompareTag("Player"))
        {
            if (temperatureCoroutine != null) // 코루틴이 실행 중인 경우
                StopCoroutine(temperatureCoroutine);
            temperatureCoroutine = StartCoroutine(IncreaseTemperature());
        }
    }

    private IEnumerator DecreaseTemperature()
    {
        while (true) // 무한 루프
        {
            if (condition != null)
            {
                condition.DecreaseTemperature(temperatureAmount);
            }
            yield return new WaitForSeconds(1f); // 1초마다 감소
        }
    }

    private IEnumerator IncreaseTemperature()
    {
        while (true) // 무한 루프
        {
            if (condition != null && condition.temperature.curValue < 36f)
            {
                // 온도가 36도 미만인 경우에만 증가
                condition.IncreaseTemperature(temperatureAmount);
            }
            else
            {
                // 온도가 36도가 되었을 때 코루틴 종료
                yield break;
            }
            yield return new WaitForSeconds(1f); // 1초마다 증가
        }
    }
}
