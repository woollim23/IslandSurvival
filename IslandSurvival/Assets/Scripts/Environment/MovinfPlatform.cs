using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // 움직이는 시작, 끝, 스피드
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public float speed = 2.0f;

    // 각 지점에서 대기할 시간 기본 설정
    public float waitTime = 2.0f;

    // 이동할 지점
    private Vector3[] positions;

    // 현재 목표 지점
    private int currentTargetIndex = 0;

    private void Start()
    {
        positions = new Vector3[] { pointA.position, pointB.position, pointC.position };
        StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {
        while (true)
        {
            Vector3 targetPosition = positions[currentTargetIndex];

            // 플랫폼을 목표 위치까지 이동
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            // 목표 위치에서 멈추고 대기
            yield return new WaitForSeconds(waitTime);

            // 다음 목표 위치로 변경
            currentTargetIndex = (currentTargetIndex + 1) % positions.Length;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))  // Player태그가 있는 오브젝트와 충돌하면
        {
            other.transform.parent = this.transform;  // Player가  플랫폼의 자식이 됨, 같이 움직이게 됨
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;  // Player가 벗어나면 부모관계 해제
        }
    }

}