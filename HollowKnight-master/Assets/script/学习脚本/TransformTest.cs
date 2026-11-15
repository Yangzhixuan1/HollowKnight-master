using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTest : MonoBehaviour
{
    void Start()
    {
        Vector2 v = new Vector2();
        v.x = 10; //第一种
        v.y = 10;
        Vector2 v2 = new Vector2(10,10); //第二种，默认不写就是0，最常用
        v2 = v + v2 ; //运算符的重载 +-*/
        v2 = v2 / 1 ;

        //常用朝向世界的
        print(Vector3.zero); // 000
        print(Vector3.right); //100
        print(Vector3.left);//-100
        print(Vector3.forward);//001 
        print(Vector3.back);//00-1
        print(Vector3.up);//010
        print(Vector3.down);//0-10 

        //计算两个点之间的距离
        print(Vector3.Distance(v,v2));

        //位置信息
        //这个是世界坐标系的原点位置，不是父子关系，界面显示的是相对父对象的
        print(this.gameObject.transform.position) ;  //或者用this.transform.position来获取
        //这个是相对父对象的位置，如果没有父对象就是世界原点
        print(this.gameObject.transform.localPosition) ;  

        //修改
        //不能单独该位置只能整体改

        this.transform.localPosition = new Vector3 (10 ,10, 10);
        this.transform.localPosition = this.transform.localPosition + Vector3.up * 10 ; //没有没有前面加起来就是0，10 ， 0 因为是010乘以10
        //如果要只改一个部分
        this.transform.localPosition = new Vector3 (this.transform.localPosition.x , 10 ,this.transform.localPosition.z);
        //或者将位置信息取出来，再修改
        Vector3 vpos = this.transform.localPosition;
        vpos.y = 100 ;
        this.transform.localPosition = vpos; //别忘了放回去
        

        //朝向
        print(this.transform.forward); // 当前的的头顶朝向，旋转后
        print(this.transform.up);
        print(this.transform.right);
        //用于后续角色面朝向运动而不是依据世界朝向


    }
}
