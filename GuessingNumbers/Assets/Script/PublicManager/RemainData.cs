using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����洢һЩ��̬�Ĺ��ñ����������������нű�ʹ��
/// </summary>
public class RemainData : MonoBehaviour
{
    /// <summary>
    /// �������ֵ
    /// </summary>
    public static int Max;
    /// <summary>
    /// ������Сֵ
    /// </summary>
    public static int Min=1;
    /// <summary>
    /// ��ǰ��Ϸ�Ѷ�
    /// </summary>
    public static ConstantData.Difficulty difficulty;
    /// <summary>
    /// ��ǰ��Ϸģʽ
    /// </summary>
    public static ConstantData.Model model;
    /// <summary>
    /// ��ǰʣ���������
    /// </summary>
    public static int Times;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
