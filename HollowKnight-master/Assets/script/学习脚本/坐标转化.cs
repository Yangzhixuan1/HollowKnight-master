using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class 坐标转化 : MonoBehaviour
{
    // Start is called before the first frame update
    /*
    void Start()
    {
        //世界转本地
        //将括号内转化为本地坐标系的位置（原点不变）受到缩放影响
        print(this.transform.InverseTransformPoint(Vector3.forward));
        //用于判断相对方位，比如x大于0就是在他的左边
        
        //方向转化
        //不受缩放影响
        print(this.transform.InverseTransformDirection(Vector3.forward));
        //收到缩放影响
        print(this.transform.InverseTransformVector(Vector3.forward));



        //本地转世界常用，收到缩放影响
        //最重要
        print(this.transform.TransformPoint(Vector3.forward)); //将自己前面的点坐标转为世界坐标，比如在自己面前放地刺

        //本地方向转世界方向
        print(this.transform.TransformDirection(Vector3.forward));//不受缩放影响
        print(this.transform.TransformVector(Vector3.forward));//受缩放影响

    }
    */

    void Start()
    {
        Transform dog = GameObject.Find("dog").transform;
        if (dog != null)
        {
            dog.transform.position = this.transform.TransformPoint(new Vector3(1,0,1));
            
        }
        
    }
}
