using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 角色攻击脚本引用


public class 角色移动 : MonoBehaviour
{
    private Rigidbody2D rb ;

    private Animator animator;

    private CapsuleCollider2D coll;

    private SpriteRenderer sprite;

    private float dirX = 0f ;

    // 角色攻击脚本引用
    private 攻击 playerAttack;

    [SerializeField] float movespeed = 2f ;
    [SerializeField] float jumpspeed = 2f ;
    [SerializeField] private LayerMask jumpableGround; //可跳跃的图层

    enum E_state
    {
        idle ,

        runuing ,

        jumping ,

        falling ,

        dash ,

        hurt ,

        cure ,

        attact
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        coll = GetComponent<CapsuleCollider2D>();

        sprite = GetComponent<SpriteRenderer>();

        playerAttack = GetComponent<攻击>();

    }


    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * movespeed , rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGround())
        {
            rb.velocity = new Vector2(rb.velocity.x , jumpspeed);
        }

        ChangState();

        if (Input.GetKey(KeyCode.B) && IsGround())
        {
            animator.SetInteger("state" , (int)E_state.cure);
        }
        
    }


    void ChangState()
    {
        E_state state = E_state.idle;

        if (dirX > 0.1f)
        {
            state = E_state.runuing;
            sprite.flipX = true;
        }
        else if (dirX < -0.1f)
        {
            state = E_state.runuing;
            sprite.flipX = false;
        }
        else
        {
            state = E_state.idle;
        }
        if (rb.velocity.y > 1.0f)
        {
            state = E_state.jumping;
        }
        else if (rb.velocity.y < -1.0f)
        {
            state = E_state.falling;
        }
        // 没有攻击时，才允许移动脚本更新动画状态
        if (playerAttack == null || !playerAttack.IsAttacking)
        {
            animator.SetInteger("state", (int)state);
        }
    }

    bool IsGround()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }


    
}
