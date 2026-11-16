using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class 速度脚本 : MonoBehaviour
{
    public float Speech = 1.0f ;

    public GameObject obj1;
    

    void Start()
    {
        //相对世界
        print(this.transform.eulerAngles);
        //相对父对象
        print(this.transform.localEulerAngles);
        
        //类似速度修改
        this.transform.eulerAngles = new Vector3(10,90,90);
        this.transform.localEulerAngles = new Vector3(10,90,90);
    }
    
    void Update()
    {
        
        //位移自己计算，以方向向自己的面朝向
        this.transform.position = this.transform.position + this.transform.forward * Speech * Time.deltaTime;
        //以世界的面朝向移动
        this.transform.position = this.transform.position + Vector3.forward * Speech * Time.deltaTime;
        
        //自带的函数
        //参数一为位移多少
        //参数二为是否要世界坐标系默认不填是相对自己的坐标系
        this.transform.Translate(Vector3.forward * Speech * Time.deltaTime); //这种是面向自己
        this.transform.Translate(Vector3.forward * Speech * Time.deltaTime , Space.World); //默认是Space.self这种是面向世界
        this.transform.Translate(this.transform.forward * Speech * Time.deltaTime , Space.World);//这种是面向自己
        
        //旋转
        this.transform.Rotate(new Vector3(0,10,0) * Time.deltaTime);//与上面一样，有两个参数
        this.transform.Rotate(Vector3.up * Time.deltaTime); //up为y轴，right为x轴，forword为z，绕着某轴转
        //绕着某一点转
        //第一参数为一个点，第二参数是该点方向，第三个为旋转速度*时间
        this.transform.RotateAround(Vector3.zero,Vector3.up ,10 * Time.deltaTime );


        //缩放
        print(this.transform.localScale);//相对父对象
        print(this.transform.lossyScale);//相对世界

        //只能修改local，世界只能读不能改
        

        this.transform.localScale += new Vector3(0.01f,0.01f,0.01f);
        //没有类似上面几个的方法

        //看向一个点（世界坐标系）
        this.transform.LookAt(Vector3.zero);//看向原点，面朝y轴过去

        //看向一个对象
        this.transform.LookAt(obj1.transform);



        

    }
}
