using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class 移动测试 : MonoBehaviour
{
    public int speech;
    float InputX ;
    float InputY ;

    // Update is called once per frame
    void Update()
    {
        InputX = Input.GetAxis("Horizontal");
        InputY = Input.GetAxis("Vertical");
        this.transform.Translate(new Vector3(InputX * Time.deltaTime * speech, InputY * Time.deltaTime * speech , 0));
        
        
    }
}
