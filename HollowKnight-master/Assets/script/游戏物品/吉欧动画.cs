using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 吉欧动画 : MonoBehaviour
{
    Animator animator;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y > 0.1 || rb.velocity.y <-0.1)
        {
            animator.SetBool("isAir" , true);
        }
        else
        {
            animator.SetBool("isAir" , false);
            
        }
        
    }
}
