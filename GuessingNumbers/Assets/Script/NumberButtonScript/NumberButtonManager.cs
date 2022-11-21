using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberButtonManager : MonoBehaviour
{
    //该按钮所代表的数字
    public int Number;


    //按钮被单击时候触发的事件
    public void NumberButtonOnclick()
    {
        EventManager.CallOnClickNumberButton(Number);
    }

    private void OnEnable()
    {
        EventManager.Pausing += OnPausing;
        EventManager.Continuing += OnContinuing;
        EventManager.GameOver += OnGameOver;
    }
    private void OnDisable()
    {
        EventManager.Pausing -= OnPausing;
        EventManager.Continuing -= OnContinuing;
        EventManager.GameOver -= OnGameOver;
    }

    //游戏结束，那么这些数字按钮就不可用
    private void OnGameOver()
    {
        gameObject.GetComponent<Button>().enabled = false;
    }

    //游戏暂停，那么这些数字按钮就不可用
    private void OnPausing()
    {
        gameObject.GetComponent<Button>().enabled = false;
    }
    //游戏继续，那么这些数字按钮就恢复使用
    private void OnContinuing()
    {
        gameObject.GetComponent<Button>().enabled = true;
    }

}
