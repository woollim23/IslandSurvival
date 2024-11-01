using UnityEngine;

public class Equip : MonoBehaviour
{
    [Header("Resource Gathering")]
    public bool doesGatherResources; // 자원 채취 할 수 있는지

    [Header("Combat")]
    public bool doesDealDamage; // 공격 데미지를 줄 수 있는지
    public int damage; // 데미지 얼마만큼 줄건지
    public float attackRate; // 공격 주기
    public float attackDistance; // 최대 공격 가능 거리
    public float useAttackStamina; // 스태미나 사용량

    [Header("Stat Increase")]
    public bool doesStatIncrease; // 스탯 증가율이 있는지
    public float increase; // 증가 수치

}