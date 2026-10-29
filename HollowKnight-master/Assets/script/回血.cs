using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 回血 : MonoBehaviour
{
    [Header("血量设置")]
    public int maxHealth = 5;       // 最大血量
    public int currentHealth = 3;   // 当前血量

    [Header("回血设置")]
    public float healTime = 1f;     // 蓄力需要的时间
    public KeyCode healKey = KeyCode.F; // 回血按键

    private bool isHealing = false; // 是否正在蓄力
    private Coroutine healCoroutine;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        // 按下 F 键，开始回血
        if (Input.GetKeyDown(healKey) && !isHealing)
        {
            healCoroutine = StartCoroutine(HealCoroutine());
        }

        // 松开 F 键，取消回血
        if (Input.GetKeyUp(healKey) && isHealing)
        {
            if (healCoroutine != null)
            {
                StopCoroutine(healCoroutine);
                healCoroutine = null;
            }

            FinishHeal();

            Debug.Log("回血取消");
        }
    }

    private IEnumerator HealCoroutine()
    {
        isHealing = true;
        Debug.Log("开始专注回血");

        animator.SetBool("isHealing", true);

        // 等待蓄力时间
        yield return new WaitForSeconds(healTime);

        // 恢复 1 点血量，但不能超过最大血量
        currentHealth = Mathf.Min(currentHealth + 1, maxHealth);

        Debug.Log("回血成功，当前血量：" + currentHealth);
        
        FinishHeal();

        healCoroutine = null;
    }

    public bool IsHealing()
    {
        return isHealing;
    }

    private void FinishHeal()
    {
        isHealing = false;

        // 通知 Animator 返回正常动作
        if (animator != null)
        {
            animator.SetBool("isHealing", false);
        }

        healCoroutine = null;
    }
}
