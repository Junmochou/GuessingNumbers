using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该类会存储一些静态的公用变量，以用来给所有脚本使用
/// </summary>
public class RemainData : MonoBehaviour
{
    /// <summary>
    /// 区间最大值
    /// </summary>
    public static int Max;
    /// <summary>
    /// 区间最小值
    /// </summary>
    public static int Min=1;
    /// <summary>
    /// 当前游戏难度
    /// </summary>
    public static ConstantData.Difficulty difficulty;
    /// <summary>
    /// 当前游戏模式
    /// </summary>
    public static ConstantData.Model model;
    /// <summary>
    /// 当前剩余猜数次数
    /// </summary>
    public static int Times;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
