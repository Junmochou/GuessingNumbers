using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberButtonManager : MonoBehaviour
{
    //�ð�ť�����������
    public int Number;


    //��ť������ʱ�򴥷����¼�
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

    //��Ϸ��������ô��Щ���ְ�ť�Ͳ�����
    private void OnGameOver()
    {
        gameObject.GetComponent<Button>().enabled = false;
    }

    //��Ϸ��ͣ����ô��Щ���ְ�ť�Ͳ�����
    private void OnPausing()
    {
        gameObject.GetComponent<Button>().enabled = false;
    }
    //��Ϸ��������ô��Щ���ְ�ť�ͻָ�ʹ��
    private void OnContinuing()
    {
        gameObject.GetComponent<Button>().enabled = true;
    }

}
