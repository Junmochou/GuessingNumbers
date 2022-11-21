using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultTextWarningAnimationScript : MonoBehaviour
{
    public Animator ResultTextAnimator;

    private void OnEnable()
    {
        EventManager.WrongAnswer += OnWrongAnswer;
    }

    private void OnDisable()
    {
        EventManager.WrongAnswer -= OnWrongAnswer;
    }

    //���ž��涯��
    private void OnWrongAnswer()
    {
        ResultTextAnimator.SetBool("Warning",true);
    }

    //ֹͣ���ž��涯��
    public void StopPlayWrongAnswerAnimation()
    {
        ResultTextAnimator.SetBool("Warning", false);
    }
}
