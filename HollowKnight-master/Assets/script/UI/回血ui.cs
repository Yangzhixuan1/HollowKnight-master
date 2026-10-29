
using UnityEngine;
using UnityEngine.UI;

public class 回血ui : MonoBehaviour
{
    [Header("引用回血脚本")]
    public 回血 playerHealth; // 拖入角色身上的回血脚本

    [Header("爱心图片")]
    public Image[] hearts; // 按顺序放入 5 个爱心 Image

    [Header("爱心状态图片")]
    public Sprite fullHeart;  // 满血爱心图片
    public Sprite emptyHeart; // 空心爱心图片

    private int lastHealth = -1; // 记录上次显示的血量

    private void Update()
    {
        if (playerHealth == null)
            return;

        // 只有血量变化时才刷新 UI
        if (lastHealth != playerHealth.currentHealth)
        {
            UpdateHearts();
            lastHealth = playerHealth.currentHealth;
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            // 当前爱心序号小于当前血量，就显示满血图标
            if (i < playerHealth.currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
