using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public event Action onAttackEvent; // 공격 애니 이벤트

    private bool attacking;

    private Camera _camera;

    private PlayerController controller;
    private PlayerCondition condition;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        controller.onAttackAction += OnAttackInput;
    }

    public void OnAttackInput(Equip equip)
    {
        if (!attacking && equip != null)
        {
            if (condition.UseStamina(equip.useAttackStamina))
            {
                onAttackEvent?.Invoke();
                attacking = true;
                Invoke("OnCanAttack", equip.attackRate);
                Debug.Log("공격!");
                //공격 조건 맞을 시 OnHit
                OnHit(equip);
            }
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }

    public void OnHit(Equip equip)
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        // Ray ray = new Ray(CharacterManager.Instance.Player.transform.position + Vector3.up * 1.5f, CharacterManager.Instance.Player.transform.forward); // 약간 위쪽에서 레이 발사
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, equip.attackDistance * 2) && hit.collider.name != "Player")
        {

            // 자원 채취 기능이 있는 장비(도끼,곡괭이)라면
            if (equip.doesGatherResources && hit.collider.TryGetComponent(out Resource resource))
            {
                resource.Gather(hit.point, hit.normal);
                Debug.Log("자원채취");
            }

            // Enemy라면 데미지 입히기
            if (equip.doesDealDamage && hit.collider.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakePhysicalDamage(equip.damage);
                Debug.Log("적 공격");

                //대상이 Animal이고 체력이 0 이하일 경우 사망 처리
                if (damagable is Animal animal && animal.health <= 0)
                {
                    animal.Die();
                }
            }

            
        }
    }
}