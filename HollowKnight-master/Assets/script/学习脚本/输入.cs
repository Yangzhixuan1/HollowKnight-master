using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 输入 : MonoBehaviour
{
    void Update()
    {
        /*
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
            //print("按下w键");
        }

        //默认轴输入
        print(Input.GetAxis("Horizontal")); //获取水平轴输入返回水平输入，1到-1之间的浮点数
        print(Input.GetAxis("Vertical"));
        //鼠标输入
        print(Input.GetAxis("Mouse X")) ;//鼠标横向运动
        print(Input.GetAxis("Mouse Y")) ;//鼠标竖向运动
        
        //GetAxis 不会有中间值,用法一样
        print(Input.GetAxisRaw("Horizontal"));
        */

        //其他方法
        //任意键
        if(Input.anyKeyDown)
        {
            print("有一个键被按下");
            print(Input.inputString); //按下的是什么键，一般配合使用空格和鼠标也可以查，显示为空
        }
        if(Input.anyKey)
        {
            print("有一个键被长按");
        }

        //主机游戏按键略，写了也记不住去看43节课7分钟

        //移动设备触摸 也记不住43节8分钟

        //陀螺仪

    }
}
