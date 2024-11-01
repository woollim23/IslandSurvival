using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool attacking;

    private Animator animator;
    private Camera _camera;

    private PlayerController controller;
    private PlayerCondition condition;

    private void Awake()
    {
        _camera = Camera.main;
        //animator = GetComponent<Animator>();
    }

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        controller.onAttackAction += OnAttackInput;
    }

    public void OnAttackInput(Equip equip)
    {
        if (!attacking)
        {
            if (condition.UseStamina(equip.useAttackStamina))
            {
                attacking = true;
                //animator.SetTrigger("Attack");
                Invoke("OnCanAttack", equip.attackRate);
                
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
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, equip.attackDistance))
        {
            // 자원 채취 기능이 있는 장비(도끼,곡괭이)라면
            if (equip.doesGatherResources && hit.collider.TryGetComponent(out Resource resource))
            {
                resource.Gather(hit.point, hit.normal);
                Debug.Log("자원채취");
            }

            // Enemy라면 데미지 입히기
            if (hit.collider.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakePhysicalDamage(equip.damage);
                Debug.Log("적 공격");

                // 대상이 Enemy(NPC)이고 체력이 0 이하일 경우 사망 처리
                //if (damagable is NPC npc && npc.health <= 0)
                //{
                //    npc.Die();
                //}
            }
        }
    }
}