using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 输入 : MonoBehaviour
{
    void Update()
    {
        //print(Input.mousePosition);//鼠标位置，左下角为原点只有xy
        if (Input.GetMouseButtonDown(0)) //0左键，1右键，2中键 up抬起down按下没有修饰就是每一帧就进入返回是bool
        {
            print("左键按下");
        }
        //鼠标中键
        //返回xy，y=-1为向下，+1为向上，0为不动
        //print(Input.mouseScrollDelta); 

        //键盘输入
        if(Input.GetKeyDown(KeyCode.W)) //同样有up和没有修饰
        {
            print("按下w键");
        }
        //不推荐这种重载，只能填小写
        if(Input.GetKeyDown("w"))
        {
            print("按下w键");
        }

        //默认轴输入
    }
}
