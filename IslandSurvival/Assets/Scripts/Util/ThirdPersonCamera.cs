using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // ī�޶� ���� ĳ����
    public float distance = 2.0f; // ĳ���Ϳ� ī�޶� �� �Ÿ�
    public float height = 1.0f;   // ī�޶��� ����
    public float rotationSpeed = 5.0f; // ȸ�� �ӵ�
    public float lookSensitivity = 2.0f; // ���콺 �ΰ���
    public float minXLook = -30.0f; // ī�޶� ���� ���� ���� (�Ʒ�)
    public float maxXLook = 60.0f;  // ī�޶� ���� ���� ���� (��)

    private float currentXRotation = 0.0f; // ���� ī�޶� ���� ����
    private float currentYRotation = 0.0f; // ���� ī�޶� �¿� ����

    void Start()
    {
        // �ʱ� ī�޶� ���� ����
        currentYRotation = target.eulerAngles.y;
    }

    void LateUpdate()
    {
        // ���콺 �Է��� �޾� ȸ�� ���� ���
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        currentYRotation += mouseX * rotationSpeed;
        currentXRotation -= mouseY * rotationSpeed;
        currentXRotation = Mathf.Clamp(currentXRotation, minXLook, maxXLook);

        // ī�޶� ��ġ �� ���� ����
        Quaternion rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
        Vector3 targetPosition = target.position - (rotation * Vector3.forward * distance) + (Vector3.up * height);

        // ī�޶� ��ġ�� ȸ�� ����
        transform.position = targetPosition;
        transform.LookAt(target.position + Vector3.up * height / 2);
    }
}
