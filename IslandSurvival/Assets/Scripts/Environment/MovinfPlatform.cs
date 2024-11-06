using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // �����̴� ����, ��, ���ǵ�
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public float speed = 2.0f;

    // �� �������� ����� �ð� �⺻ ����
    public float waitTime = 2.0f;

    // �̵��� ����
    private Vector3[] positions;

    // ���� ��ǥ ����
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

            // �÷����� ��ǥ ��ġ���� �̵�
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            // ��ǥ ��ġ���� ���߰� ���
            yield return new WaitForSeconds(waitTime);

            // ���� ��ǥ ��ġ�� ����
            currentTargetIndex = (currentTargetIndex + 1) % positions.Length;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))  // Player�±װ� �ִ� ������Ʈ�� �浹�ϸ�
        {
            other.transform.parent = this.transform;  // Player��  �÷����� �ڽ��� ��, ���� �����̰� ��
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;  // Player�� ����� �θ���� ����
        }
    }

}