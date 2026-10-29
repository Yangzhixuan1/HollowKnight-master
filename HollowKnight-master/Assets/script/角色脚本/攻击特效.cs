
using System.Collections;
using UnityEngine;

public class 攻击特效 : MonoBehaviour
{
    [Header("特效设置")]
    public float effectDuration = 0.2f; // 特效持续时间
    public Vector3 rightOffset = new Vector3(0.8f, 0f, 0f); // 面朝右时的位置
    public Vector3 leftOffset = new Vector3(-0.8f, 0f, 0f); // 面朝左时的位置
    public Vector3 upOffset = new Vector3(-0.8f, 0f, 0f); // 面朝上时的位置
    public Vector3 downOffset = new Vector3(-0.8f, 0f, 0f); // 面朝下时的位置

    private SpriteRenderer spriteRenderer;
    private Coroutine effectCoroutine;

    public enum E_hitDir
    {
        up, 
        down, 
        right,
        left,
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 开始时隐藏特效
        gameObject.SetActive(false);
    }

    public void PlayEffect(int e_HitDir)

    // 播放攻击特效，传入角色朝向
    //public void PlayEffect(bool facingRight)
    {
        switch (e_HitDir)
        {
            case 0 :
            transform.localPosition = upOffset;
            // 相对于父物体设置旋转角度
            spriteRenderer.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
            break;
            case 1 :
            transform.localPosition = downOffset;
            spriteRenderer.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            break;
            case 2 :
            transform.localPosition = rightOffset;
            spriteRenderer.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            break;
            case 3 :
            transform.localPosition = leftOffset;
            spriteRenderer.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            break;
        }

        // 激活物体后开始播放
        gameObject.SetActive(true);

        if (effectCoroutine != null)
        {
            StopCoroutine(effectCoroutine);
        }

        effectCoroutine = StartCoroutine(HideAfterTime());
    }

    private IEnumerator HideAfterTime()
    {
        // 等待特效播放时间
        yield return new WaitForSeconds(effectDuration);

        // 隐藏特效
        gameObject.SetActive(false);
        effectCoroutine = null;
    }
}
