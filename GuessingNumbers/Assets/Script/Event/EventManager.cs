using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �������ڹ�����Ϸ�ڵĸ����¼�
/// </summary>
public class EventManager : MonoBehaviour
{
    //�¼�����Ϸģʽ���Ѷ�����ѡ���С� ���¼��ᴫ���ѶȺ�ģʽ��������
    public static event Action<ConstantData.Difficulty, ConstantData.Model> SelectingModelAndDifficulty;
    public static void CallSelectingModelAndDifficulty(ConstantData.Difficulty difficulty,ConstantData.Model model)
    {
        if (SelectingModelAndDifficulty!=null)
        {
            SelectingModelAndDifficulty.Invoke(difficulty,model);
        }
    }

    //�¼�����������ּ����ϵ�����ְ�ť�����¼�����һ�����ͱ�����������ȡ������ʲô����
    public static event Action<int> ClickNumberButton;
    public static void CallOnClickNumberButton(int Number)
    {
        if (ClickNumberButton!=null)
        {
            ClickNumberButton.Invoke(Number);
        }
    }

    //�¼����������ͣ��Ϸ�������¼�
    public static event Action Pausing;
    public static void CallPausing()
    {
        if (Pausing!=null)
        {
            Pausing.Invoke();
        }
    }

    //�¼�������ڼ�����Ϸ��ʱ��ᴥ�����¼�
    public static event Action Continuing;
    public static void CallContinuing()
    {
        if (Continuing!=null)
        {
            Continuing.Invoke();
        }
    }

    //�¼�����Ϸ������ֻ������Ϸ������ʱ��ᴥ�����¼�
    public static event Action GameOver;
    public static void CallGameOver()
    {
        if (GameOver!=null)
        {
            GameOver.Invoke();
        }
    }

    //�¼�����Ҳ´��˻ᴥ�����¼�
    public static event Action WrongAnswer;
    public static void CallWrongAnswer()
    {
        if (WrongAnswer!=null)
        {
            WrongAnswer.Invoke();
        }
    }
    
}
