using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������ͳһһЩ���õ�������
/// ������Ϸģʽ����Ϸ�Ѷ���Щ
/// </summary>
public class ConstantData : MonoBehaviour
{
    //ö�٣���Ϸ�Ѷ�
    public enum Difficulty
    {
        Simple,
        Normal,
        Difficult
    }

    //ö�٣���Ϸģʽ
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
