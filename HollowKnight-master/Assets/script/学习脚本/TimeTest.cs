using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //时间缩放
        Time.timeScale = 0; //时间静止
        Time.timeScale = 2; //二倍数
        //帧间隔时间
        //1收到scale影响
        print(Time.deltaTime);//两帧之间时间

        //2不受到scale影响1
        print(Time.unscaledDeltaTime); 
        //选择，如果要不受暂停影响就用2，收到暂停影响就用1

        //游戏开始到现在的时间（单机游戏）
        print(Time.time);
        print(Time.unscaledTime);
        print(Time.frameCount); //一共跑了多少次循环
    }
    //物理帧时间
    private void FixedUpdate()
    {
        print(Time.fixedDeltaTime);
        print(Time.fixedUnscaledDeltaTime);
    }
}
