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
            if (equip.doesGatherResources && hit.collider.TryGetComponent(out Resource resource))
            {
                resource.Gather(hit.point, hit.normal);
            }
        }
    }
}