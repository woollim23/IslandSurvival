using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 캐릭터
    public float distance = 2.0f; // 캐릭터와 카메라 간 거리
    public float height = 1.0f;   // 카메라의 높이
    public float rotationSpeed = 5.0f; // 회전 속도
    public float lookSensitivity = 2.0f; // 마우스 민감도
    public float minXLook = -30.0f; // 카메라 상하 각도 제한 (아래)
    public float maxXLook = 60.0f;  // 카메라 상하 각도 제한 (위)

    private float currentXRotation = 0.0f; // 현재 카메라 상하 각도
    private float currentYRotation = 0.0f; // 현재 카메라 좌우 각도

    void Start()
    {
        // 초기 카메라 각도 설정
        currentYRotation = target.eulerAngles.y;
    }

    void LateUpdate()
    {
        // 마우스 입력을 받아 회전 값을 계산
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        currentYRotation += mouseX * rotationSpeed;
        currentXRotation -= mouseY * rotationSpeed;
        currentXRotation = Mathf.Clamp(currentXRotation, minXLook, maxXLook);

        // 카메라 위치 및 각도 설정
        Quaternion rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
        Vector3 targetPosition = target.position - (rotation * Vector3.forward * distance) + (Vector3.up * height);

        // 카메라 위치와 회전 적용
        transform.position = targetPosition;
        transform.LookAt(target.position + Vector3.up * height / 2);
    }
}
