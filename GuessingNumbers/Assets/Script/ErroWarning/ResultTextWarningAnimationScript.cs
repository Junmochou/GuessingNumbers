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

    //播放警告动画
    private void OnWrongAnswer()
    {
        ResultTextAnimator.SetBool("Warning",true);
    }

    //停止播放警告动画
    public void StopPlayWrongAnswerAnimation()
    {
        ResultTextAnimator.SetBool("Warning", false);
    }
}
