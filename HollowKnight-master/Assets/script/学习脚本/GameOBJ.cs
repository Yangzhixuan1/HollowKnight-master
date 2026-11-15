using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class GameOBJ : MonoBehaviour
{
    //准备克隆的对象
    [Header("你要克隆的对象")]
    public GameObject cloneOBJ;
    // Start is called before the first frame update
    void Start()
    {
        //获取一些基本信息
        print(gameObject.name);
        gameObject.name = "新名字";
        print(gameObject.activeSelf); //获取激活状态
        print(gameObject.isStatic); //静态状态信息
        print(gameObject.layer); //层级信息，int
        print(gameObject.tag);//标签，string
        print(gameObject.transform.position);
        print(transform.rotation);//旋转
        print(transform.lossyScale);//缩放
        //基本的静态方法
        //GameObject.CreatePrimitive(PrimitiveType.Cube);//创建原始的物体，参数是枚举类型 
        //也可以用新变量来接住他
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        print(obj.name);
        obj.name = "正方体";
        obj.SetActive(false);
        //查找对象
        //1. 查找单个对象,无法找到失活对象,多个对象不确定找到的是谁
        //1.1 根据name查找，效率低一般不用
        GameObject obj2 = GameObject.Find("钱");
        //1.2 根据tag来查找
        obj2 = GameObject.FindWithTag("Player"); 
        obj2 = GameObject.FindGameObjectWithTag("Player");//与上面一个一样
        
        if(obj2 != null)
        {
            print(obj2.name);
        }
        else
        {
            print("没有找到");
        }
        //2. 查找多个对象，无法通过名字查找
        GameObject[] obj3 = GameObject.FindGameObjectsWithTag("Player");
        print("找到了" + obj3.Length + "个对象");
        //3. 找脚本来达到找该脚本身上的物体,效率最低，一般不用
        攻击特效 gj = GameObject.FindAnyObjectByType<攻击特效>();
        if (gj != null)
        {
            print(gj.gameObject.name);
        }
        //克隆对象，根据已经有的对象，来创建出新的
        //可以场上有的对象，或者预设体
        GameObject obj4 = GameObject.Instantiate(cloneOBJ);//可以用东西去接住他
        obj4.name = "钱克隆";
        //删除对象
        GameObject.Destroy(obj4); //删除
        GameObject.Destroy(this.gameObject , 5); //第二个参数为延迟秒数
        Destroy(this);//可以删除这个脚本

        //立即删除,上面会延迟一帧，避免卡顿
        GameObject.DestroyImmediate(obj4);

        //过场景不移除
        GameObject.DontDestroyOnLoad(this.gameObject);

        //创建空物体
        GameObject obj5 = new GameObject("我创建的空物体");//参数是名字
        GameObject obj6 = new GameObject("带脚本的空物体" , typeof(攻击) ,typeof(AudioClip));//后面几个参数是不定的，是自带的脚本

        //为已经有的物体加脚本

        obj6.AddComponent(typeof(攻击)); //可以用东西接住他
        攻击 gj2 = obj6.AddComponent(typeof(攻击)) as 攻击;
        攻击 gj3 = obj6.AddComponent<攻击>(); //一般用这个

        //对比标签,两种一样
        if (this.gameObject.CompareTag("Player"))
        {
            print("第一种比较标签");
        }
        if(this.gameObject.tag == "Player")
        {
            print("第二种比较tag");
        }

        //设置失活激活
        obj6.SetActive(false);
        
        //一些不建议使用

        //查找自身带有的脚本中的方法，所有同名的都执行
        //不如直接获取脚本之后用
        this.gameObject.SendMessage("TestFun");
        this.gameObject.SendMessage("TestFun2" , 199);//如果是有参数的函数这样写

        //gameObject.BroadcastMessage("函数名");//告诉子对象和自己
        //gameObject.SendMessageUpwards("函数名");//告诉父对象和自己
    }
    void TestFun()
    {
        print("123");
    }
    void TestFun2(int num)
    {
        print(num);
    }
}
