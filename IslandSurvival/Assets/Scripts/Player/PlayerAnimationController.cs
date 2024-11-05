using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private static readonly int isRun = Animator.StringToHash("isRun");
    private static readonly int isJump = Animator.StringToHash("isJump");
    private static readonly int isAttack = Animator.StringToHash("isAttack");
    private static readonly int isDead = Animator.StringToHash("isDead");

    private readonly float magnituteThreshold = 0.1f;


    private void Start()
    {
        CharacterManager.Instance.Player.controller.onMoveEvent += MoveAnim;
        CharacterManager.Instance.Player.controller.onJumpEvent += JumpAnim;
        CharacterManager.Instance.Player.controller.onAttackEvent += AttackAnim;

    }

    private void Update()
    {
        // TODO : 그라운드 체크
        if (CharacterManager.Instance.Player.controller.IsGrounded())
            CharacterManager.Instance.Player.animator.SetBool(isJump, false);
    }

    private void MoveAnim(bool move)
    {
        CharacterManager.Instance.Player.animator.SetBool(isRun, move);
    }
    private void JumpAnim()
    {
        CharacterManager.Instance.Player.animator.SetBool(isJump, true);
    }

    public void AttackAnim()
    {
        CharacterManager.Instance.Player.animator.SetBool(isAttack, true);
    }
    public void DeadAnim()
    {
        CharacterManager.Instance.Player.animator.SetBool(isDead, true);
    }
}
