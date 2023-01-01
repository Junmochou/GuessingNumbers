
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
    [Header("������ʾ��С���")]
    public GameObject TipsPnael;
    [Header("��Ϸ������ʾ���ı�")]
    public Text ExtraTipsText;

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
                    ClickTimes = 0;
                    Num = 0;
                    InputText.text = Num.ToString();
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

    //��Ϸ�����¼�,���ұ�����Ϸ����
    private void OnGameOver()
    {
        GameOverPanel.SetActive(true);
        if (isSuccessful)
        {
            InformationText.text = "����������ɹ�\n\n����������" + (SpareTimes - RemainData.Times).ToString() + "��\n\n���մ𰸣�"+RandNumber.ToString();
            if (RemainData.difficulty == ConstantData.Difficulty.Simple && RemainData.model == ConstantData.Model.Limit)
            {
                PlayerPrefs.SetInt(ConstantData.DataName[0], PlayerPrefs.GetInt(ConstantData.DataName[0],0)+1);
                PlayerPrefs.SetFloat(ConstantData.DataName[0]+"1", PlayerPrefs.GetFloat(ConstantData.DataName[0]+"1", 0) + (float)SpareTimes-RemainData.Times);
                PlayerPrefs.Save();
            }
            else if (RemainData.difficulty == ConstantData.Difficulty.Normal &&RemainData.model == ConstantData.Model.Limit)
            {
                PlayerPrefs.SetInt(ConstantData.DataName[1], PlayerPrefs.GetInt(ConstantData.DataName[1], 0) + 1);
                PlayerPrefs.SetFloat(ConstantData.DataName[1] + "1", PlayerPrefs.GetFloat(ConstantData.DataName[1] + "1", 0) + (float)SpareTimes - RemainData.Times);
                PlayerPrefs.Save();
            }
            else if (RemainData.difficulty == ConstantData.Difficulty.Difficult &&RemainData.model == ConstantData.Model.Limit)
            {
                PlayerPrefs.SetInt(ConstantData.DataName[2], PlayerPrefs.GetInt(ConstantData.DataName[2], 0) + 1);
                PlayerPrefs.SetFloat(ConstantData.DataName[2] + "1", PlayerPrefs.GetFloat(ConstantData.DataName[2] + "1", 0) + (float)SpareTimes - RemainData.Times);
                PlayerPrefs.Save();
            }
            else if (RemainData.difficulty == ConstantData.Difficulty.Simple && RemainData.model == ConstantData.Model.Limitless)
            {
                PlayerPrefs.SetInt(ConstantData.DataName[3], PlayerPrefs.GetInt(ConstantData.DataName[3], 0) + 1);
                PlayerPrefs.SetFloat(ConstantData.DataName[3] + "1", PlayerPrefs.GetFloat(ConstantData.DataName[3] + "1", 0) + (float)SpareTimes - RemainData.Times);
                PlayerPrefs.Save();
            }
            else if (RemainData.difficulty == ConstantData.Difficulty.Normal && RemainData.model == ConstantData.Model.Limitless)
            {
                PlayerPrefs.SetInt(ConstantData.DataName[4], PlayerPrefs.GetInt(ConstantData.DataName[4], 0) + 1);
                PlayerPrefs.SetFloat(ConstantData.DataName[4] + "1", PlayerPrefs.GetFloat(ConstantData.DataName[4] + "1", 0) + (float)SpareTimes - RemainData.Times);
                PlayerPrefs.Save();
            }
            else if (RemainData.difficulty == ConstantData.Difficulty.Difficult && RemainData.model == ConstantData.Model.Limitless)
            {
                PlayerPrefs.SetInt(ConstantData.DataName[5], PlayerPrefs.GetInt(ConstantData.DataName[5], 0) + 1);
                PlayerPrefs.SetFloat(ConstantData.DataName[5] + "1", PlayerPrefs.GetFloat(ConstantData.DataName[5] + "1", 0) + (float)SpareTimes - RemainData.Times);
                PlayerPrefs.Save();
            }

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

    //�������ʾ
    public void ExtraTips()
    {
        if (RemainData.Times-2<=0)
        {
            EventManager.CallWrongAnswer();
            ResultText.text = "�����������㣡";
        }
        else
        {
            RemainData.Times -= 2;
            RemainTimesText.text = "ʣ�������" + RemainData.Times.ToString() + "��";
            string[] result = { "����", "����","����","ż��" };
            int index = UnityEngine.Random.Range(0, 4);
            if (index == 0)
            {
                int sum = 0;
                for (int i = 1; i <= RandNumber; i++)
                {
                    if (RandNumber% i == 0)
                    {
                        sum += 1;
                    }
                }
                if (sum == 2)
                {
                    ExtraTipsText.text="����һ������";
                }
                else
                {
                    ExtraTipsText.text = "������һ������";
                }
            }
            else if (index==1)
            {
                int sum = 0;
                for (int i = 1; i <= RandNumber; i++)
                {
                    if (RandNumber % i == 0)
                    {
                        sum += 1;
                    }
                }
                if (sum > 2)
                {
                    ExtraTipsText.text = "����һ������";
                }
                else
                {
                    ExtraTipsText.text = "������һ������";
                }
            }
            else if (index == 2||index==3)
            {
                if (RandNumber%2==0)
                {
                    ExtraTipsText.text = "����һ��ż��";
                }
                else
                {
                    ExtraTipsText.text = "����һ������";
                }
            }
            TipsPnael.SetActive(true);
        }
        
    }

    public void CloseTipsPanel()
    {
        TipsPnael.SetActive(false);
    }
}
