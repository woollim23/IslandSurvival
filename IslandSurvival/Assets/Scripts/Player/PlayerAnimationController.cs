using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerAnimationController : MonoBehaviour
{
    private static readonly int isRun = Animator.StringToHash("isRun");
    private static readonly int isJump = Animator.StringToHash("isJump");
    private static readonly int isHit = Animator.StringToHash("isAttack");
    private static readonly int isDead = Animator.StringToHash("isDead");

    private readonly float magnituteThreshold = 0.1f;


    private void Start()
    {
       // CharacterManager.Instance.Player.controller.OnMoveEvent += MoveAnim;
       // CharacterManager.Instance.Player.controller.OnJumpEvent += JumpAnim;

    }

    private void Update()
    {
        // TODO : 그라운드 체크
        // if (CharacterManager.Instance.Player.controller)
            // CharacterManager.Instance.Player.animator.SetBool(isJump, false);
    }

    private void MoveAnim(Vector2 vector)
    {
        // CharacterManager.Instance.Player.animator.SetBool(isRun, vector.magnitude > magnituteThreshold);
    }
    private void JumpAnim(bool obj)
    {
        // CharacterManager.Instance.Player.animator.SetBool(isJump, obj);
    }

    public void HitAnim()
    {
        // CharacterManager.Instance.Player.animator.SetBool(isHit, true);
    }
    public void DeadAnim()
    {
        // CharacterManager.Instance.Player.animator.SetBool(isDead, true);
    }
}
