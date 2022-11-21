using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    //游戏模式提示文本对象
    public GameObject TipsText;

    //游戏难度选择面板对象
    public GameObject DifficultySelectPanel;

    //游戏模式选择面板对象
    public GameObject ModelSelectPanel;

    //储存玩家选择的游戏难度
    private ConstantData.Difficulty GameDifficulty;

    //储存玩家选择的游戏模式
    private ConstantData.Model GameModel;

    //关于游戏的面板
    public GameObject AboutPanel;

    //游戏版本号文本框
    public Text VersionText;



    private void Start()
    {
        VersionText.text = "当前版本：" + Application.version;
    }
    //打开游戏模式提示文本
    public void OpenTipsText()
    {
        TipsText.SetActive(TipsText);
    }

    //打开游戏难度选择面板
    public void OpenDifficultySelectPanel()
    {
        DifficultySelectPanel.SetActive(true);
    }

    //关闭游戏难度选择面板
    public void CloseDifficultySelectPanel()
    {
        DifficultySelectPanel.SetActive(false);
    }

    /// <summary>
    /// 游戏模式选择面板在游戏难度选择之后显示，
    /// 因此需要先关闭游戏难度选择面板。
    /// </summary>
    //打开游戏模式选择面板
    public void OpenModelSelectPanel()
    {
        CloseDifficultySelectPanel();
        ModelSelectPanel.SetActive(true);
    }

    //关闭游戏模式选择面板
    public void CloseModelSelectPanel()
    {
        ModelSelectPanel.SetActive(false);
    }

    //打开关于游戏的面板
    public void OpenAboutPanel()
    {
        AboutPanel.SetActive(true);
    }

    //简单模式的按钮被单击
    public void SimpleButtonOnclick()
    {
        GameDifficulty = ConstantData.Difficulty.Simple;
    }
    //普通模式的按钮被单击
    public void NormalButtonOnclick()
    {
        GameDifficulty = ConstantData.Difficulty.Normal;
    }
    //困难模式按钮被单击
    public void DifficultButtonOnclick()
    {
        GameDifficulty = ConstantData.Difficulty.Difficult;
    }

    //限次模式按钮被单击
    public void LimitButtonOnclick()
    {
        GameModel = ConstantData.Model.Limit;
        EventManager.CallSelectingModelAndDifficulty(GameDifficulty, GameModel);
        SwitchToGameScene();
    }
    //不限次模式按钮被单击
    public void LimitlessButtonOnclick()
    {
        GameModel = ConstantData.Model.Limitless;
        EventManager.CallSelectingModelAndDifficulty(GameDifficulty,GameModel);
        SwitchToGameScene();
    }

    //切换到游戏场景
    public void SwitchToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    //退出游戏的按钮被单击事件
    public void OnFinishButtonClick()
    {
        #if UNITY_EDITOR //编辑器中退出游戏
            UnityEditor.EditorApplication.isPlaying = false;
        #else //应用程序中退出游戏
	        UnityEngine.Application.Quit();
        #endif
    }

    private void OnEnable()
    {
        EventManager.SelectingModelAndDifficulty += OnSelecting;
    }

    private void OnDisable()
    {
        EventManager.SelectingModelAndDifficulty -= OnSelecting;
    }

    /// <summary>
    /// 在游戏主页触发选择游戏模式和难度的事件
    /// </summary>
    private void OnSelecting(ConstantData.Difficulty difficulty, ConstantData.Model model)
    {
        RemainData.difficulty = difficulty;
        RemainData.model = model;
    }
}
