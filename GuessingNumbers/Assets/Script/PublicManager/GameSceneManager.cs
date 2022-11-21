using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [Header("��ʾ���������ı��ؼ�")]
    //��Ϸ�������ʾ������ı�
    public Text InputText;
    [Header("��Ϸ�������")]
    //��Ϸ�������
    public GameObject GameOverPanel;
    [Header("��Ϸ��ͣ���")]
    //��Ϸ��ͣ���
    public GameObject PausingPanel;
    [Header("��Ϸ��ʾ�ı�")]
    public Text TipsText;
    [Header("��Ϸ��������ı�")]
    public Text ResultText;
    [Header("��Ϸ��������Ϣ�ı�")]
    public Text InformationText;
    [Header("��Ϸ���������ı�")]
    public Text RemainTimesText;

    /// <summary>
    /// ����Ҫ�������Ϸ��Ҫ�ı���
    /// </summary>
    //������������
    private int Num=0;
    //��Ұ����ְ�ť�Ĵ���
    private int ClickTimes = 0;
    //��Ϸ�Ƿ����
    private bool IsGameOver = false;
    //������ı���
    private int RandNumber;
    //��Ϸ��� ����Ϊ����ˣ�����Ϊʧ����
    private bool isSuccessful = false;
    //���ݲ�������
    private int SpareTimes;

    private void Start()
    {
        InitialGame(RemainData.difficulty, RemainData.model);
        SpareTimes = RemainData.Times;
        TipsText.text = "�����ķ�Χ��" + RemainData.Min.ToString() + "-" + RemainData.Max.ToString() + "֮��";
        RemainTimesText.text = "ʣ�������" + RemainData.Times.ToString() + "��";
    }

    //��ȡ�����ֵ�λ��
    private int GetNumberSize(int num)
    {
        int result = 0;
        for (int i=num;i>0;i/=10)
        {
            result++;
        }
        return result;
    }
    
    //���¼�������û������������
    private void OnClickNumberButton(int Number)
    {
        //�����û����������λ�����ó���6λ��
        if (GetNumberSize(Num) <= 6)
        {
            Num = Num*10+Number;
            InputText.text = Num.ToString();
            ClickTimes++;
        }
    }

    //�û�����˸�����¼�
    public void OnClickBackSpaceButton()
    {
        if (Num == 0)
        {
            Num = Num + 0;
        }
        else
        {
            Num = Num / 10;
        }

        InputText.text = Num.ToString();

        if (ClickTimes<=0)
        {
            ClickTimes = 0;
        }
        else
        {
            ClickTimes--;
        }
       
    }

    //�û�����ύ�������¼�
    public void OnClickSubmitButton()
    {
        //û����Ϸ�������ܽ�����֤
        if (!IsGameOver)
        {
            if (Num==RandNumber)
            {
                RemainData.Times--;
                isSuccessful = true;
                IsGameOver = true;
                EventManager.CallGameOver();
            }
            else
            {
                RemainData.Times--;
                if (RemainData.Times<=0)
                {
                    isSuccessful = false;
                    IsGameOver = true;
                    EventManager.CallGameOver();
                }
                else
                {
                    CompareBigger(Num);
                    TipsText.text = "������Χ��" + RemainData.Min.ToString() + "-" + RemainData.Max.ToString() + "֮��";
                    EventManager.CallWrongAnswer();
                    ResultText.text = "��´���";
                }
            }
            RemainTimesText.text = "ʣ�������" + RemainData.Times.ToString() + "��";
        }
    }
    //�Ƚ����Ĵ�С���ҽ��иı�
    private void CompareBigger(int num)
    {
        //�����������ֱ���С�ڵ��ڵ�ǰ��ʾ���ֵ���ܽ�����Ч����ʾ
        if (num <= RemainData.Max&&num>=RemainData.Min)
        {
            if (num > RandNumber)
            {
                RemainData.Max = num;
            }
            else if (num<RandNumber)
            {
                RemainData.Min = num;
            }
        }
    }
    //���ذ�ť���������¼�
    public void OnReturnButtonClick()
    {
        //ֻ�в�����Ϸ�����ſ��Դ򿪷��ذ�ť
        if (IsGameOver==false)
        {
            PausingPanel.SetActive(true);
        }
    }

    //OnEnable��OnDisable��������ע���ע���¼�
    private void OnEnable()
    {
        EventManager.ClickNumberButton += OnClickNumberButton;
        EventManager.GameOver += OnGameOver;
    }
    private void OnDisable()
    {
        EventManager.ClickNumberButton -= OnClickNumberButton;
        EventManager.GameOver -= OnGameOver;
    }

    //��Ϸ�����¼�
    private void OnGameOver()
    {
        GameOverPanel.SetActive(true);
        if (isSuccessful)
        {
            InformationText.text = "����������ɹ�\n\n����������" + (SpareTimes - RemainData.Times).ToString() + "��\n\n���մ𰸣�"+RandNumber.ToString();

        }
        else
        {
            InformationText.text = "���������ʧ��\n\n����������" + (SpareTimes - RemainData.Times).ToString() + "��\n\n���մ𰸣�"+RandNumber.ToString();
        }
    }

    //��ʼ����Ϸ�����ֵ����
    public void InitialGame(ConstantData.Difficulty difficulty, ConstantData.Model model)
    {
        /*
         * ������Ϸ��ģʽ����ʼ������
         * ��������޴�ģʽ���ͳ�ʼ��Ϊ0��
         * ����͸�����Ϸ�Ѷ�����ʼ��������ֵ
         * */
        RemainData.Min = 1;
        if (model==ConstantData.Model.Limitless)
        {
            RemainData.Times = 10000;
            switch (difficulty)
            {
                case ConstantData.Difficulty.Simple:
                    RandNumber = UnityEngine.Random.Range(1, 101);
                    RemainData.Max = 100;
                    break;
                case ConstantData.Difficulty.Normal:
                    RandNumber = UnityEngine.Random.Range(1, 1001);
                    RemainData.Max = 1000;
                    break;
                case ConstantData.Difficulty.Difficult:
                    RandNumber = UnityEngine.Random.Range(1, 10001);
                    RemainData.Max = 10000;
                    break;
            }
        }
        else
        {
            switch (difficulty) 
            {
                case ConstantData.Difficulty.Simple:
                    RandNumber = UnityEngine.Random.Range(1, 101);
                    RemainData.Max = 100;
                    RemainData.Times = 10;
                    break;
                case ConstantData.Difficulty.Normal:
                    RandNumber = UnityEngine.Random.Range(1,1001);
                    RemainData.Max = 1000;
                    RemainData.Times = 15;
                    break;
                case ConstantData.Difficulty.Difficult:
                    RandNumber = UnityEngine.Random.Range(1, 10001);
                    RemainData.Max = 10000;
                    RemainData.Times = 20;
                    break;
            }

        }


    }

    //�л���������
    public void SwitchToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    //���¿�ʼ��Ϸ
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
