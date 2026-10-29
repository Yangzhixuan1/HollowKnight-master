
using System.Collections;
using System.Data.Common;
using UnityEngine;

public class 攻击 : MonoBehaviour
{
    [Header("攻击设置")]
    public KeyCode attackKey = KeyCode.J; // 攻击按键
    public float attackDuration = 0.4f;   // 攻击持续时间，需按动画长度调整
    public int attackState = 7;           // Animator 中攻击状态对应的整数

    private Animator animator;            // 角色动画控制器
    private bool isAttacking = false;     // 是否正在攻击

    private 回血 healScript;

    // 提供给移动脚本读取，避免移动动画覆盖攻击动画
    public bool IsAttacking => isAttacking;

    [Header("攻击特效")]
    public 攻击特效 attackEffect; // 拖入攻击特效物体

    public SpriteRenderer playerSprite; // 角色自身的 SpriteRenderer

    [Header("特效时间")]
    public float effectDelay = 0.2f; // 攻击开始后多久出现特效

    enum E_hitDir
    {
        up, 
        down, 
        right,
        left,
    }

    private void Awake()
    {
        // 获取角色身上的 Animator
        animator = GetComponent<Animator>();
        healScript = GetComponent<回血>();

        if (animator == null)
        {
            Debug.LogError("PlayerAttack：没有找到 Animator 组件！");
        }
    }


    private void Update()
    {
        // 按下攻击键，且当前没有攻击时，开始攻击
        if (Input.GetKeyDown(attackKey) && !isAttacking && !healScript.IsHealing())
        {
            E_hitDir e_HitDir = E_hitDir.right;
            
            if (Input.GetKey(KeyCode.W))
            {
                e_HitDir = E_hitDir.up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                e_HitDir = E_hitDir.down;
            }
            else if (!playerSprite.flipX)
            {
                e_HitDir = E_hitDir.right;
            }
            else
            {
                e_HitDir = E_hitDir.left;
            }
            StartCoroutine(AttackCoroutine((int)e_HitDir));
        }
    }

    private IEnumerator AttackCoroutine(int e_HitDir)
    {
        if (animator == null)
            yield break;

        // 标记攻击状态
        isAttacking = true;

        // 将 Animator 切换到攻击状态
        animator.SetInteger("state", attackState);

        yield return new WaitForSeconds(effectDelay); //

        if (attackEffect != null && playerSprite != null)
        {
            attackEffect.PlayEffect(e_HitDir);
        }
        // 等待攻击动画对应的时间
        yield return new WaitForSeconds(attackDuration);

        // 解除攻击锁定，允许移动脚本重新控制动画
        isAttacking = false;
    }
}
