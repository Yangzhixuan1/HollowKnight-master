# Unity 常用类与 API 学习笔记

> 面向 Unity 初学者，结合 Unity 2D
> 游戏开发整理。建议先理解类的用途，再记常用属性和方法，不必一次背完所有
> API。

## 目录

1.  [先理解 Unity 的基本结构](#1-先理解-unity-的基本结构)
2.  [核心基础类](#2-核心基础类)
3.  [游戏逻辑与检测类](#3-游戏逻辑与检测类)
4.  [UI 常用类](#4-ui-常用类)
5.  [动画、音频与特效类](#5-动画音频与特效类)
6.  [生命周期与协程](#6-生命周期与协程)
7.  [常见组合示例](#7-常见组合示例)
8.  [推荐学习顺序](#8-推荐学习顺序)
9.  [学习时容易混淆的概念](#9-学习时容易混淆的概念)

------------------------------------------------------------------------

## 1. 先理解 Unity 的基本结构

修改

Unity 场景中的玩家、敌人、摄像机、灯光和 UI 对象，通常都是一个
`GameObject`。

可以把它理解成一个"容器"：

-   **GameObject**：游戏物体本身。
-   **Component（组件）**：给物体添加具体功能。
-   **Transform**：每个 GameObject 都有的组件，负责位置、旋转和缩放。
-   **脚本**：通常是继承 `MonoBehaviour`
    的组件，可以编写自己的游戏逻辑。

例如，Player 这个 GameObject 可以挂载：

  组件                             作用
  -------------------------------- ----------------------
  Transform                        控制位置、旋转、缩放
  SpriteRenderer                   显示 2D 图片
  Rigidbody2D                      参与 2D 物理模拟
  BoxCollider2D                    进行 2D 碰撞检测
  Animator                         控制动画
  PlayerController（自定义脚本）   编写玩家控制逻辑

### 获取组件的基本写法

``` csharp
using UnityEngine;

public class PlayerExample : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake()
    {
        // 获取当前 GameObject 上的 Rigidbody2D 组件
        rb = GetComponent<Rigidbody2D>();
    }
}
```

`GetComponent<T>()` 中的 `T` 表示要获取的组件类型。比如
`GetComponent<Animator>()` 就是获取 Animator。

------------------------------------------------------------------------

## 2. 核心基础类

### 2.1 GameObject：游戏物体

**用途：** 创建、查找、激活、禁用游戏物体，以及获取或添加组件。

常用属性：

  ---------------------------------------------------------------------------------------
  属性                    作用                           示例
  ----------------------- ------------------------------ --------------------------------
  `name`                  获取或修改物体名称             `gameObject.name = "Player";`

  `tag`                   获取或设置标签                 `gameObject.tag`

  `layer`                 获取或设置图层编号             `gameObject.layer = 3;`

  `activeSelf`            物体自身是否被激活             `gameObject.activeSelf`

  `activeInHierarchy`     物体在场景层级中是否实际激活   `gameObject.activeInHierarchy`

  `transform`             获取 Transform 组件            `gameObject.transform`

  `isStatic`              是否标记为静态物体             `gameObject.isStatic`

  `scene`                 所属场景                       `gameObject.scene`
  ---------------------------------------------------------------------------------------

常用方法：

  方法                          作用
  ----------------------------- ------------------------------
  `SetActive(bool)`             激活或禁用整个 GameObject
  `GetComponent<T>()`           获取当前物体上的一个指定组件
  `GetComponents<T>()`          获取当前物体上的多个指定组件
  `TryGetComponent<T>(out T)`   尝试获取组件，成功返回 true
  `AddComponent<T>()`           运行时添加组件
  `CompareTag(string)`          判断物体是否带有指定 Tag
  `GetInstanceID()`             获取对象实例 ID

示例：

``` csharp
// 隐藏整个面板
panel.SetActive(false);

// 判断碰到的物体是不是金币
if (other.gameObject.CompareTag("Coin"))
{
    // 销毁金币
    Destroy(other.gameObject);
}

// 获取当前物体上的 Animator
Animator anim = GetComponent<Animator>();
```

查找物体的常见静态方法：

  ----------------------------------------------------------------------------------
  方法                                           作用
  ---------------------------------------------- -----------------------------------
  `GameObject.Find("Player")`                    按名称查找一个活动物体

  `GameObject.FindWithTag("Enemy")`              按 Tag 查找一个物体

  `GameObject.FindGameObjectWithTag("Enemy")`    按 Tag 查找一个物体

  `GameObject.FindGameObjectsWithTag("Enemy")`   查找所有匹配 Tag 的物体
  ----------------------------------------------------------------------------------

**注意：** 不要在 `Update()` 中反复使用 `Find()`
查找对象。通常在初始化时查找一次，或者通过 Inspector 拖拽引用。`Find`
类方法也有查找范围和激活状态方面的限制。

### 2.2 Transform：位置、旋转与缩放

**用途：** 控制物体在场景中的空间信息。几乎所有 GameObject 都有
Transform。

常用属性：

  属性                       作用
  -------------------------- ------------------------
  `position`                 世界坐标位置
  `localPosition`            相对父物体的位置
  `rotation`                 世界旋转（Quaternion）
  `localRotation`            相对父物体的旋转
  `eulerAngles`              世界欧拉角表示的旋转
  `localEulerAngles`         局部欧拉角
  `localScale`               相对父物体的缩放
  `parent`                   父 Transform
  `childCount`               子物体数量
  `forward`、`right`、`up`   物体局部坐标系的方向

常用方法：

  方法                作用
  ------------------- -----------------------------
  `Translate()`       按方向移动
  `Rotate()`          旋转
  `LookAt()`          让物体朝向目标（常用于 3D）
  `SetParent()`       设置父物体
  `Find()`            按路径查找子 Transform
  `GetChild(index)`   获取指定索引的子 Transform

示例：

``` csharp
// 设置世界坐标
transform.position = new Vector3(0, 2, 0);

// 设置相对父物体的位置
transform.localPosition = new Vector3(1, 0, 0);

// 设置缩放
transform.localScale = new Vector3(2, 2, 1);

// 获取第一个子物体
Transform child = transform.GetChild(0);
```

### 2.3 Component：组件基类

**用途：** Unity 中许多组件类的基类。`Transform`、`Collider`、`Renderer`
等都属于组件体系。

常用成员：

  成员                            作用
  ------------------------------- ---------------------------
  `gameObject`                    获取组件所在的 GameObject
  `transform`                     获取所在物体的 Transform
  `GetComponent<T>()`             获取同一物体上的组件
  `GetComponentInChildren<T>()`   在自身及子物体中查找组件
  `GetComponentInParent<T>()`     在自身及父物体中查找组件

示例：

``` csharp
// 当前脚本继承 MonoBehaviour，也属于 Component
GameObject owner = gameObject;
Transform myTransform = transform;
```

### 2.4 MonoBehaviour：脚本常用基类

**用途：** 大多数需要挂到 GameObject 上运行的自定义脚本，都继承
`MonoBehaviour`。

它提供生命周期方法、协程、组件访问等能力。常见方法见第 6 节。

------------------------------------------------------------------------

## 3. 游戏逻辑与检测类

### 3.1 Input：输入

**用途：** 获取键盘、鼠标等输入。以下是旧输入系统（Input Manager）的常见
API。

  API                                 作用
  ----------------------------------- ------------------------
  `Input.GetKeyDown(KeyCode.Space)`   按键刚按下时为 true
  `Input.GetKey(KeyCode.Space)`       按住期间持续为 true
  `Input.GetKeyUp(KeyCode.Space)`     按键刚松开时为 true
  `Input.GetButtonDown("Jump")`       输入按钮刚按下
  `Input.GetAxis("Horizontal")`       获取平滑的水平输入值
  `Input.GetAxisRaw("Horizontal")`    获取较直接的水平输入值
  `Input.GetMouseButtonDown(0)`       鼠标左键刚按下

示例：

``` csharp
// 获取水平输入，通常 A/D 或左右方向键对应 -1 到 1
float dir = Input.GetAxisRaw("Horizontal");

// 空格键刚按下时触发一次
if (Input.GetKeyDown(KeyCode.Space))
{
    Debug.Log("跳跃");
}
```

**注意：** Unity 也有新版 Input
System。项目使用哪套输入系统，要看项目设置和实际配置；不要把两套系统的用法混为一谈。

### 3.2 Physics2D：2D 物理检测

**用途：** 进行 2D 射线、形状检测和重叠检测。

  API                           作用
  ----------------------------- ----------------------------
  `Physics2D.Raycast()`         发射 2D 射线
  `Physics2D.BoxCast()`         发射盒形检测
  `Physics2D.CircleCast()`      发射圆形检测
  `Physics2D.OverlapCircle()`   检查圆形范围内是否有碰撞体
  `Physics2D.OverlapBox()`      检查盒形范围内是否有碰撞体
  `Physics2D.Linecast()`        检测两点之间的线段

示例：

``` csharp
// 从角色当前位置向右检测 3 个单位
RaycastHit2D hit = Physics2D.Raycast(
    transform.position,
    Vector2.right,
    3f
);

if (hit.collider != null)
{
    Debug.Log("检测到了：" + hit.collider.gameObject.name);
}
```

### 3.3 Collider2D：2D 碰撞体

**用途：** 定义物体在 2D 物理系统中的碰撞形状。

常见子类：

-   `BoxCollider2D`：矩形碰撞体。
-   `CircleCollider2D`：圆形碰撞体。
-   `CapsuleCollider2D`：胶囊形碰撞体。
-   `PolygonCollider2D`：多边形碰撞体。

常用属性：

  属性          作用
  ------------- ------------------------------------
  `isTrigger`   是否作为触发器，不产生普通物理阻挡
  `enabled`     是否启用碰撞体
  `bounds`      世界空间中的包围范围
  `offset`      碰撞形状相对物体的偏移

常见回调：

``` csharp
void OnCollisionEnter2D(Collision2D other)
{
    // 与非 Trigger 碰撞体发生碰撞时调用
}

void OnTriggerEnter2D(Collider2D other)
{
    // 进入 Trigger 区域时调用
}
```

### 3.4 Rigidbody2D：2D 刚体

**用途：** 让物体参与 2D 物理模拟，例如重力、速度、力和碰撞响应。

常用属性和方法：

  API                作用
  ------------------ ----------------------------------
  `velocity`         读取或设置线速度（较早版本常用）
  `linearVelocity`   新版本中使用的线速度属性
  `gravityScale`     重力缩放
  `mass`             质量
  `AddForce()`       添加力
  `AddTorque()`      添加扭矩
  `MovePosition()`   在物理更新中移动刚体
  `bodyType`         刚体类型

**版本提醒：** Unity 2022 常见写法是 `Rigidbody2D.velocity`；较新 Unity
版本提供并推荐 `linearVelocity`。请以当前 Unity 版本的 API 为准。

### 3.5 Time：时间

  API                     作用
  ----------------------- ------------------------------
  `Time.deltaTime`        距离上一帧经过的时间
  `Time.time`             游戏开始后经过的游戏时间
  `Time.timeScale`        时间速度倍率，0 通常表示暂停
  `Time.fixedDeltaTime`   固定物理更新间隔

示例：

``` csharp
// 让移动速度不受帧率变化影响
transform.position += Vector3.right * speed * Time.deltaTime;
```

### 3.6 Random：随机数

  API                         作用
  --------------------------- --------------------------
  `Random.Range(min, max)`    生成指定范围随机值
  `Random.value`              生成 0 到 1 之间的随机数
  `Random.insideUnitCircle`   获取单位圆内随机点

``` csharp
// int 重载：可能取到 min 和 max
int index = Random.Range(0, 5);

// float 重载：返回 min（含）到 max（不含）之间的值
float value = Random.Range(0f, 1f);
```

### 3.7 Camera：摄像机

  API                      作用
  ------------------------ ----------------------------------
  `Camera.main`            获取带有 MainCamera 标签的摄像机
  `orthographicSize`       正交摄像机的视野尺寸
  `fieldOfView`            透视摄像机的视野角度
  `ScreenToWorldPoint()`   屏幕坐标转世界坐标
  `WorldToScreenPoint()`   世界坐标转屏幕坐标

### 3.8 SceneManager：场景管理

需要引用：

``` csharp
using UnityEngine.SceneManagement;
```

  API                               作用
  --------------------------------- --------------------
  `SceneManager.LoadScene()`        加载场景
  `SceneManager.LoadSceneAsync()`   异步加载场景
  `SceneManager.GetActiveScene()`   获取当前活动场景
  `SceneManager.sceneCount`         获取已加载场景数量

------------------------------------------------------------------------

## 4. UI 常用类

UI 类通常位于 `UnityEngine.UI` 或 TextMeshPro 命名空间中。

### 4.1 Image：UI 图片

引用：

``` csharp
using UnityEngine.UI;
```

常用属性：

  属性               作用
  ------------------ ------------------------------------------
  `sprite`           获取或更换显示的 Sprite
  `color`            修改图片颜色和透明度
  `enabled`          启用或禁用 Image 组件
  `type`             Simple、Sliced、Tiled、Filled 等图片类型
  `fillAmount`       Filled 类型的填充比例，通常为 0 到 1
  `fillMethod`       填充方式
  `fillOrigin`       填充起点
  `preserveAspect`   是否保持图片宽高比例
  `raycastTarget`    是否参与 UI 射线检测
  `rectTransform`    获取自身的 RectTransform

常用方法：

  方法                 作用
  -------------------- --------------------------------
  `SetNativeSize()`    将尺寸设置为 Sprite 的原始尺寸
  `CrossFadeAlpha()`   在指定时间内渐变透明度
  `CrossFadeColor()`   在指定时间内渐变颜色

示例：

``` csharp
public Image heartImage;
public Sprite fullHeart;
public Sprite emptyHeart;

void UpdateHeart(bool hasHealth)
{
    // 根据是否有血量切换爱心图片
    heartImage.sprite = hasHealth ? fullHeart : emptyHeart;
}
```

血条示例：

``` csharp
public Image healthImage;

void UpdateHealth(float currentHealth, float maxHealth)
{
    // 避免最大血量为 0 时除以 0
    if (maxHealth <= 0) return;

    // Image Type 需要在 Inspector 中设置为 Filled
    healthImage.fillAmount = currentHealth / maxHealth;
}
```

### 4.2 TextMeshProUGUI：TMP UI 文字

引用：

``` csharp
using TMPro;
```

常用属性：

  属性                 作用
  -------------------- ----------------------
  `text`               设置显示文字
  `fontSize`           字体大小
  `color`              字体颜色
  `alignment`          对齐方式
  `enableAutoSizing`   是否自动调整字体大小

``` csharp
public TextMeshProUGUI coinText;
int coinCount = 0;

void AddCoin()
{
    coinCount++;
    // 更新金币数量
    coinText.text = "金币：" + coinCount;
}
```

### 4.3 Button：按钮

引用：

``` csharp
using UnityEngine.UI;
```

常用成员：

  成员             作用
  ---------------- --------------
  `onClick`        按钮点击事件
  `interactable`   是否允许交互

``` csharp
public Button startButton;

void Start()
{
    // 代码绑定按钮点击事件
    startButton.onClick.AddListener(StartGame);
}

void StartGame()
{
    Debug.Log("开始游戏");
}
```

也可以直接在 Inspector 的 Button → On Click() 中绑定方法。

### 4.4 Slider：滑动条

常用属性：

  属性               作用
  ------------------ ----------------
  `value`            当前值
  `minValue`         最小值
  `maxValue`         最大值
  `wholeNumbers`     是否只允许整数
  `onValueChanged`   数值变化事件

``` csharp
public Slider volumeSlider;

void Start()
{
    // 监听滑动条数值变化
    volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
}

void OnVolumeChanged(float value)
{
    Debug.Log("当前音量：" + value);
}
```

### 4.5 RectTransform：UI 位置和尺寸

**用途：** RectTransform 是 UI 对象常用的 Transform 类型。

  属性                       作用
  -------------------------- ------------------------------
  `anchoredPosition`         相对锚点的位置
  `sizeDelta`                尺寸差值，具体表现受锚点影响
  `anchorMin`、`anchorMax`   锚点范围
  `pivot`                    轴心点
  `localScale`               局部缩放
  `rotation`                 旋转

``` csharp
RectTransform rect = heartImage.rectTransform;

// 设置 UI 的锚点相对位置
rect.anchoredPosition = new Vector2(0, 50);

// 设置尺寸差值
rect.sizeDelta = new Vector2(100, 100);
```

### 4.6 Canvas：画布

常用属性：

-   `renderMode`：画布渲染模式。
-   `worldCamera`：用于渲染画布的摄像机。
-   `scaleFactor`：画布缩放系数。

------------------------------------------------------------------------

## 5. 动画、音频与特效类

### 5.1 SpriteRenderer：2D 图片渲染

**用途：** 显示场景中的 2D Sprite。它与 UI 的 `Image`
不同：SpriteRenderer 通常用于场景物体，Image 通常用于 Canvas UI。

  属性               作用
  ------------------ ---------------------------------
  `sprite`           当前显示的 Sprite
  `color`            图片颜色
  `flipX`、`flipY`   水平或垂直翻转
  `sortingOrder`     同一 Sorting Layer 中的排序顺序
  `enabled`          是否启用组件

### 5.2 Animator：动画控制器

  API                               作用
  --------------------------------- ----------------------
  `SetBool()`                       设置布尔参数
  `SetInteger()`                    设置整数参数
  `SetFloat()`                      设置浮点数参数
  `SetTrigger()`                    触发 Trigger 参数
  `GetBool()`                       获取布尔参数
  `GetCurrentAnimatorStateInfo()`   获取当前动画状态信息
  `Play()`                          播放指定状态

``` csharp
public Animator anim;

void Attack()
{
    // 触发 Animator 中名为 Attack 的 Trigger 参数
    anim.SetTrigger("Attack");
}
```

### 5.3 AudioSource：音频播放组件

  API / 属性        作用
  ----------------- --------------------------
  `Play()`          播放音频
  `PlayOneShot()`   播放一次音效，适合短音效
  `Stop()`          停止播放
  `Pause()`         暂停
  `UnPause()`       继续播放
  `volume`          音量
  `loop`            是否循环
  `clip`            当前音频片段
  `pitch`           音调和播放速度相关参数

### 5.4 AudioClip：音频资源

  属性          作用
  ------------- ----------------
  `length`      音频时长（秒）
  `frequency`   采样频率
  `samples`     音频采样数量
  `channels`    声道数

AudioClip 是音频资源；AudioSource 是负责播放它的组件。

### 5.5 ParticleSystem：粒子系统

  API / 属性     作用
  -------------- ----------------
  `Play()`       播放粒子
  `Stop()`       停止发射或播放
  `Clear()`      清除粒子
  `isPlaying`    是否正在播放
  `isEmitting`   是否正在发射

### 5.6 TrailRenderer：拖尾

  属性 / 方法   作用
  ------------- ------------------
  `emitting`    是否继续产生拖尾
  `time`        拖尾保留时间
  `Clear()`     清除已有拖尾

------------------------------------------------------------------------

## 6. 生命周期与协程

### 6.1 常见生命周期方法

这些方法由 Unity 在特定时机自动调用，不需要自己手动调用。

  方法              大致调用时机               常见用途
  ----------------- -------------------------- ----------------------
  `Awake()`         脚本实例加载时             获取组件、初始化引用
  `OnEnable()`      组件启用时                 注册事件
  `Start()`         第一次启用后、首次更新前   开始游戏逻辑
  `Update()`        每帧调用                   输入、非物理更新
  `FixedUpdate()`   固定时间间隔调用           物理相关更新
  `LateUpdate()`    Update 之后调用            摄像机跟随等
  `OnDisable()`     组件禁用时                 取消事件注册
  `OnDestroy()`     对象销毁时                 清理资源或事件

### 6.2 Invoke：延迟调用

``` csharp
void Start()
{
    // 2 秒后调用 RestartLevel 方法
    Invoke(nameof(RestartLevel), 2f);
}

void RestartLevel()
{
    Debug.Log("重新开始");
}
```

### 6.3 Coroutine：协程

协程可以让一段代码暂停一段时间后继续执行，常用于延迟、持续回血、攻击间隔等。

``` csharp
using System.Collections;
using UnityEngine;

public class HealExample : MonoBehaviour
{
    IEnumerator Heal()
    {
        Debug.Log("开始回血");

        // 等待 1 秒后继续执行
        yield return new WaitForSeconds(1f);

        Debug.Log("回血完成");
    }

    void Start()
    {
        // 启动协程
        StartCoroutine(Heal());
    }
}
```

------------------------------------------------------------------------

## 7. 常见组合示例

### 7.1 玩家移动：Input + Rigidbody2D

``` csharp
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;

    private float direction;

    void Update()
    {
        // 获取水平输入
        direction = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        // 在物理更新中设置刚体水平速度
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
}
```

> 这是适合 Unity 2022 的常见写法。较新版本也可能使用 `linearVelocity`。

### 7.2 金币拾取：Collider2D + CompareTag + Destroy

``` csharp
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // 判断进入触发区域的物体是否为玩家
        if (other.CompareTag("Player"))
        {
            // 销毁金币所在的 GameObject
            Destroy(gameObject);
        }
    }
}
```

> 使用 Trigger 回调时，需要正确配置 Collider2D 的 Is Trigger，并满足
> Unity 2D 物理回调对 Rigidbody2D 等组件的要求。

### 7.3 攻击动画和音效：Animator + AudioSource

``` csharp
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioSource;
    public AudioClip attackClip;

    void Update()
    {
        // 按下 J 键触发攻击
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("Attack");

            // 播放攻击音效
            audioSource.PlayOneShot(attackClip);
        }
    }
}
```

### 7.4 血量 UI：Image + RectTransform

``` csharp
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthImage;

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (maxHealth <= 0) return;

        // 更新血条填充比例
        healthImage.fillAmount = currentHealth / maxHealth;
    }
}
```

------------------------------------------------------------------------

## 8. 推荐学习顺序

按照 2D 横版游戏开发的常见需求，可以按以下顺序学习：

  ------------------------------------------------------------------------------------------------------------------------------------
  阶段                    建议掌握的类                                                                学习目标
  ----------------------- --------------------------------------------------------------------------- --------------------------------
  第一阶段：基础          `GameObject`、`Transform`、`Component`、`MonoBehaviour`                     理解物体、组件和脚本

  第二阶段：角色控制      `Input`、`Rigidbody2D`、`Collider2D`、`Physics2D`                           移动、跳跃、碰撞、射线检测

  第三阶段：角色表现      `SpriteRenderer`、`Animator`、`AudioSource`、`ParticleSystem`               图片、动画、音效、特效

  第四阶段：UI            `Image`、`Button`、`Slider`、`TextMeshProUGUI`、`RectTransform`、`Canvas`   血量、菜单、设置界面

  第五阶段：游戏管理      `SceneManager`、`PlayerPrefs`、`Time`、`Random`、`Camera`                   场景切换、数据保存、时间和镜头
  ------------------------------------------------------------------------------------------------------------------------------------

建议每学完一个类，就写一个很小的测试场景验证它的属性和方法。

------------------------------------------------------------------------

## 9. 学习时容易混淆的概念

### 9.1 Image 和 SpriteRenderer

  Image                         SpriteRenderer
  ----------------------------- -------------------------------
  常用于 Canvas 内的 UI         常用于场景中的 2D 物体
  类型是 UI 组件                类型是 Renderer 组件
  常用 `sprite`、`fillAmount`   常用 `sprite`、`sortingOrder`
  例如血量图标、按钮图片        例如玩家、敌人、场景道具

### 9.2 GameObject 和 Component

-   GameObject 是物体。
-   Component 是附加在物体上的功能。
-   `GetComponent<T>()` 是从 GameObject 或组件所在物体上获取指定组件。

### 9.3 SetActive 和 enabled

-   `gameObject.SetActive(false)`：禁用整个
    GameObject，其组件也会停止正常工作。
-   `component.enabled = false`：只禁用指定组件，GameObject
    本身仍然可以处于激活状态。

### 9.4 activeSelf 和 activeInHierarchy

-   `activeSelf`：只看物体自身的激活开关。
-   `activeInHierarchy`：还要考虑父物体是否激活。父物体被禁用时，子物体即使
    `activeSelf` 为 true，也可能不在场景中实际激活。

### 9.5 Update 和 FixedUpdate

-   `Update()`：每帧调用，常用于读取输入和普通逻辑。
-   `FixedUpdate()`：按固定时间间隔调用，常用于刚体物理操作。
-   不要把所有代码都放进 `FixedUpdate()`；也不要在 `Update()`
    中无节制地修改物理刚体。

### 9.6 属性和方法的区别

-   **属性**：通常像变量一样读取或赋值，例如
    `image.sprite`、`transform.position`。
-   **方法**：执行某个动作，通常带括号，例如
    `SetActive(false)`、`Play()`、`AddForce()`。

------------------------------------------------------------------------

## 最后：建议建立自己的 API 速查表

学习每个类时，可以用下面的模板记录：

``` text
类名：
命名空间：
主要用途：

常用属性：
1.
2.
3.

常用方法：
1.
2.
3.

实际使用场景：
相关类：
自己的代码示例：
```

**学习提醒：** 本文列出的是常用 API，不是完整 API 清单。Unity API
会随版本变化；遇到不确定的成员，建议查看当前 Unity 版本的官方 Scripting
API 文档。
