using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class 父对象 : MonoBehaviour
{
    public Transform son ;
    // Start is called before the first frame update
    /*
    void Start()
    {
        //.parent获取的是transform，可以点出名字  
        //print(this.transform.parent.name); 
        //断绝父子关系
        //this.transform.parent = null;
        //认父对象
        this.transform.parent = GameObject.Find("钱").transform;
         
        //通过api设置
        this.transform.SetParent(null); //里面填要认的对象
        //第二个参数是是否保留世界坐标位置,就是不变，一般要true,默认
        //this.transform.SetParent(GameObject.Find("钱").transform,true);
        this.transform.SetParent(GameObject.Find("钱").transform,false);
        //如果是false，就是将面板上的数据直接变相对坐标会很奇怪

        //删除自己的所有儿子,自己的孙子上位
        this.transform.DetachChildren();

        //查找子对象
        GameObject obj = this.transform.Find("Cube").gameObject;
        //区别gameobject的find，这个是找子对象，且这个可以找到失活的，object是不能找失活的，且找的是全局的
        //只能找儿子找不了孙子

        //遍历儿子
        print(transform.childCount); // 找到儿子的数量，包括失活，不包括孙子

        for (int i = 0 ; i < this.transform.childCount ; i++)
        {
            print(this.transform.GetChild(i).name); //将所有儿子名字打印出来
        }

        //儿子的操作

        son.IsChildOf(this.transform); //这个是不是ta的儿子，返回bool
        if (son.IsChildOf(this.transform))
        {
            print("这是我的儿子");
        }

        //得到自己作为儿子的编号

        print(son.GetSiblingIndex());

        son.SetAsFirstSibling(); //将自己设置为第一个儿子
        son.SetAsLastSibling(); //将自己设置为最后一个儿子
        son.SetSiblingIndex(3) ;//将自己设置为指定的儿子，超过最大会是最后一个



    }
    */
    //实战

    void Start()
    {
        son = GameObject.Find("白珩").transform ;
        
    }
    void Update()
    {
        transform.LookAt(son);
    }
}
