using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 甲壳虫移动脚本 : MonoBehaviour
{
    [Header("移动参数")]
    public float moveSpeed = 2f;   // 移动速度
    public float moveRange = 3f;   // 左右巡逻范围

    private SpriteRenderer sr;        // 精灵渲染器
    private Collider2D monsterCol;    // 怪物碰撞体
    
    private Vector2 startPos;         // 起始位置
    private int moveDir = 1;          // 移动方向：1右，-1左
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;                // 记录起始位置
        sr = GetComponent<SpriteRenderer>();          // 获取渲染器
        monsterCol = GetComponent<Collider2D>();      // 获取碰撞体
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(moveDir * moveSpeed * Time.deltaTime, 0, 0);
        if (transform.position.x > startPos.x + moveRange)
            moveDir = -1; // 到右边界掉头
        if (transform.position.x < startPos.x - moveRange)
            moveDir = 1;  // 到左边界掉头

        //翻转
        if (sr != null)
        {
            sr.flipX = (moveDir > 0);
        }

    }
}
