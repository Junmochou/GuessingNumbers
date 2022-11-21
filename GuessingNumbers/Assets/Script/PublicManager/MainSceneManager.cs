using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    //��Ϸģʽ��ʾ�ı�����
    public GameObject TipsText;

    //��Ϸ�Ѷ�ѡ��������
    public GameObject DifficultySelectPanel;

    //��Ϸģʽѡ��������
    public GameObject ModelSelectPanel;

    //�������ѡ�����Ϸ�Ѷ�
    private ConstantData.Difficulty GameDifficulty;

    //�������ѡ�����Ϸģʽ
    private ConstantData.Model GameModel;

    //������Ϸ�����
    public GameObject AboutPanel;

    //��Ϸ�汾���ı���
    public Text VersionText;



    private void Start()
    {
        VersionText.text = "��ǰ�汾��" + Application.version;
    }
    //����Ϸģʽ��ʾ�ı�
    public void OpenTipsText()
    {
        TipsText.SetActive(TipsText);
    }

    //����Ϸ�Ѷ�ѡ�����
    public void OpenDifficultySelectPanel()
    {
        DifficultySelectPanel.SetActive(true);
    }

    //�ر���Ϸ�Ѷ�ѡ�����
    public void CloseDifficultySelectPanel()
    {
        DifficultySelectPanel.SetActive(false);
    }

    /// <summary>
    /// ��Ϸģʽѡ���������Ϸ�Ѷ�ѡ��֮����ʾ��
    /// �����Ҫ�ȹر���Ϸ�Ѷ�ѡ����塣
    /// </summary>
    //����Ϸģʽѡ�����
    public void OpenModelSelectPanel()
    {
        CloseDifficultySelectPanel();
        ModelSelectPanel.SetActive(true);
    }

    //�ر���Ϸģʽѡ�����
    public void CloseModelSelectPanel()
    {
        ModelSelectPanel.SetActive(false);
    }

    //�򿪹�����Ϸ�����
    public void OpenAboutPanel()
    {
        AboutPanel.SetActive(true);
    }

    //��ģʽ�İ�ť������
    public void SimpleButtonOnclick()
    {
        GameDifficulty = ConstantData.Difficulty.Simple;
    }
    //��ͨģʽ�İ�ť������
    public void NormalButtonOnclick()
    {
        GameDifficulty = ConstantData.Difficulty.Normal;
    }
    //����ģʽ��ť������
    public void DifficultButtonOnclick()
    {
        GameDifficulty = ConstantData.Difficulty.Difficult;
    }

    //�޴�ģʽ��ť������
    public void LimitButtonOnclick()
    {
        GameModel = ConstantData.Model.Limit;
        EventManager.CallSelectingModelAndDifficulty(GameDifficulty, GameModel);
        SwitchToGameScene();
    }
    //���޴�ģʽ��ť������
    public void LimitlessButtonOnclick()
    {
        GameModel = ConstantData.Model.Limitless;
        EventManager.CallSelectingModelAndDifficulty(GameDifficulty,GameModel);
        SwitchToGameScene();
    }

    //�л�����Ϸ����
    public void SwitchToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    //�˳���Ϸ�İ�ť�������¼�
    public void OnFinishButtonClick()
    {
        #if UNITY_EDITOR //�༭�����˳���Ϸ
            UnityEditor.EditorApplication.isPlaying = false;
        #else //Ӧ�ó������˳���Ϸ
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
    /// ����Ϸ��ҳ����ѡ����Ϸģʽ���Ѷȵ��¼�
    /// </summary>
    private void OnSelecting(ConstantData.Difficulty difficulty, ConstantData.Model model)
    {
        RemainData.difficulty = difficulty;
        RemainData.model = model;
    }
}
