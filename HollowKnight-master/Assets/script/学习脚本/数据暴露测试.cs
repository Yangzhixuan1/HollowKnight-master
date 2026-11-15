using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class 数据暴露测试 : MonoBehaviour
{
    public int age ;
    public float high;
    public string newname;
    public enum meiju
    {
        idle,
        running,
        jump,
        fall,
        attack
    }
    public Image image;
    public int[] arr = new int[4];
    public List<int> list = new List<int>();

    
    [SerializeField] private string name2;
    [Header("以下是附加知识点")]
    [TextArea(3, 5)]
    

    public string txt;
    [Tooltip("你好")]
    public bool isok;
    [Range(1,10)]
    [ContextMenuItem("方法名","ok")]
    public float length;

    void Start()
    {
        print(this.gameObject.name);
        print(this.transform.position);//
        print(this.transform.eulerAngles);//
        print(this.transform.lossyScale);//
        //3.获取脚本是否激活
        this.enabled =false;
        this.enabled =true;
        list.Add(11);


        //获取脚本的方法 如果获取失败 就是没有对应的脚本 会默认返回空
        攻击 t = this.GetComponent("攻击") as 攻击;
        print(t);
        //根据Type获取
        t = this.GetComponent(typeof(攻击)) as 攻击;
        print(t);
        //根据泛型获取建议使用泛型获取因为不用二次转换
        t = this.GetComponent<攻击>();
        print(t);
        //只要你能得到场景中别的对象或者对象依附的脚本
        //那你就可以获取到它的所有信息
        //2.得到自己挂载的多个脚本
        攻击[] array = this.GetComponents<攻击>();
        print(array.Length);
        List<攻击> list2 = new List<攻击>();
        this.GetComponents<攻击>(list2);
        print(list2.Count);
        //获取子对象的脚本（也会同时获取自己是否挂了这个脚本）
        //里面参数是是否失活的也要找
        攻击特效 t2 = this.GetComponentInChildren<攻击特效>(true);
        List<攻击> listgj = new List<攻击>();
        this.GetComponentsInChildren<攻击>(true,list2);

        //更加安全的写法
        if (this.TryGetComponent<攻击>(out t))
        {
            //逻辑处理
        }
        //其实也可以获取后进行
        if (t != null)
        {
            t.enabled = false;
            t2.enabled = false;
            //逻辑处理
        }

    }

    
    
    void ok()
    {
        length = 5;
    }
}
