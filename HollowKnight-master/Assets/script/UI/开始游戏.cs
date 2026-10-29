using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 开始游戏 : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("正式游戏");
    }
}
