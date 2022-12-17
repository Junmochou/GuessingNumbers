using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该类用于统一一些常用到的数据
/// 比如游戏模式、游戏难度这些
/// </summary>
public class ConstantData : MonoBehaviour
{
    //枚举：游戏难度
    public enum Difficulty
    {
        Simple,
        Normal,
        Difficult
    }

    //枚举：游戏模式
    public enum Model 
    {
        Limit,
        Limitless
    }

    public static string[] DataName = { 
        "Limit-Simple",
        "Limit-Normal",
        "Limit-Difficult",
        "Limitless-Simple",
        "Limitless-Normal",
        "Limitless-Difficult"
    };
}
