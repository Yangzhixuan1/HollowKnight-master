using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 屏幕 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Resolution r = Screen.currentResolution ;//当前的分辨率
        print("当前分辨率为"+r.width + "*" + r.height);
        print(Screen.width); //屏幕的宽高，不是分辨率
        print(Screen.height);

        //屏幕息屏模式
        Screen.sleepTimeout = SleepTimeout.NeverSleep ;//永不息屏（手机端）

        //不常用
        Screen.fullScreen = true;//是否运行时全屏
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen ;//独占全屏还有其他模式
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow ; //全屏窗口
        Screen.fullScreenMode = FullScreenMode.MaximizedWindow ; //最大化窗口 
        Screen.fullScreenMode = FullScreenMode.Windowed ;//窗口模式  

        //移动设备旋转
        //允许左横向
        Screen.autorotateToLandscapeLeft = true ;
        //右横向
        Screen.autorotateToLandscapeRight = true ;
        //纵向
        Screen.autorotateToPortrait = true ;
        //倒立
        Screen.autorotateToPortraitUpsideDown = true ;

        //指定屏幕方向
        Screen.orientation = ScreenOrientation.AutoRotation ;//自动旋转，也可以指定方向


        //静态方法
        //设置分辨率
        Screen.SetResolution(1920,1080,true); //最后一个参数是否全屏

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
