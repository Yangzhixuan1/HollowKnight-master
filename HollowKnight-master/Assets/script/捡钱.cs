using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 捡钱 : MonoBehaviour
{
    public static int collectedCount = 0;
    public TMPro.TextMeshProUGUI countText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 判断碰到的物体标签是否为“钱”
        if (other.gameObject.CompareTag("钱"))
        {
            // 收集数量加一
            collectedCount++;

            // 更新 UI
            UpdateUI();

            // 销毁收集品物体
            Destroy(other.gameObject);
        }
        
    }

    private void UpdateUI()
    {
        if (countText != null)
        {
            countText.text = " " + collectedCount;
        }
    }

    
}
