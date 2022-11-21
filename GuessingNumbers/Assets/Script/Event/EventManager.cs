using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 该类用于管理游戏内的各种事件
/// </summary>
public class EventManager : MonoBehaviour
{
    //事件：游戏模式和难度正在选择中。 该事件会传入难度和模式两个参数
    public static event Action<ConstantData.Difficulty, ConstantData.Model> SelectingModelAndDifficulty;
    public static void CallSelectingModelAndDifficulty(ConstantData.Difficulty difficulty,ConstantData.Model model)
    {
        if (SelectingModelAndDifficulty!=null)
        {
            SelectingModelAndDifficulty.Invoke(difficulty,model);
        }
    }

    //事件：玩家在数字键盘上点击数字按钮。该事件传入一个整型变量，用来获取按的是什么数字
    public static event Action<int> ClickNumberButton;
    public static void CallOnClickNumberButton(int Number)
    {
        if (ClickNumberButton!=null)
        {
            ClickNumberButton.Invoke(Number);
        }
    }

    //事件：玩家在暂停游戏触发此事件
    public static event Action Pausing;
    public static void CallPausing()
    {
        if (Pausing!=null)
        {
            Pausing.Invoke();
        }
    }

    //事件：玩家在继续游戏的时候会触发此事件
    public static event Action Continuing;
    public static void CallContinuing()
    {
        if (Continuing!=null)
        {
            Continuing.Invoke();
        }
    }

    //事件：游戏结束。只有在游戏结束的时候会触发该事件
    public static event Action GameOver;
    public static void CallGameOver()
    {
        if (GameOver!=null)
        {
            GameOver.Invoke();
        }
    }

    //事件：玩家猜错了会触发该事件
    public static event Action WrongAnswer;
    public static void CallWrongAnswer()
    {
        if (WrongAnswer!=null)
        {
            WrongAnswer.Invoke();
        }
    }
    
}
