
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [Header("显示玩家输入的文本控件")]
    //游戏界面的显示输入的文本
    public Text InputText;
    [Header("游戏结束面板")]
    //游戏结束面板
    public GameObject GameOverPanel;
    [Header("游戏暂停面板")]
    //游戏暂停面板
    public GameObject PausingPanel;
    [Header("游戏提示文本")]
    public Text TipsText;
    [Header("游戏结果反馈文本")]
    public Text ResultText;
    [Header("游戏结束的信息文本")]
    public Text InformationText;
    [Header("游戏次数提醒文本")]
    public Text RemainTimesText;
    [Header("额外提示的小面板")]
    public GameObject TipsPnael;
    [Header("游戏额外提示的文本")]
    public Text ExtraTipsText;

    /// <summary>
    /// 这里要定义该游戏必要的变量
    /// </summary>
    //玩家输入的数字
    private int Num=0;
    //玩家按数字按钮的次数
    private int ClickTimes = 0;
    //游戏是否结束
    private bool IsGameOver = false;
    //随机数的变量
    private int RandNumber;
    //游戏结果 真则为完成了，否则为失败了
    private bool isSuccessful = false;
    //备份猜数次数
    private int SpareTimes;

    private void Start()
    {
        InitialGame(RemainData.difficulty, RemainData.model);
        SpareTimes = RemainData.Times;
        TipsText.text = "该数的范围在" + RemainData.Min.ToString() + "-" + RemainData.Max.ToString() + "之间";
        RemainTimesText.text = "剩余次数：" + RemainData.Times.ToString() + "次";
    }

    //获取该数字的位数
    private int GetNumberSize(int num)
    {
        int result = 0;
        for (int i=num;i>0;i/=10)
        {
            result++;
        }
        return result;
    }
    
    //该事件会监听用户的输入的数字
    private void OnClickNumberButton(int Number)
    {
        //限制用户输入的数字位数不得超过6位数
        if (GetNumberSize(Num) <= 6)
        {
            Num = Num*10+Number;
            InputText.text = Num.ToString();
            ClickTimes++;
        }
    }

    //用户点击退格键的事件
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

    //用户点击提交按键的事件
    public void OnClickSubmitButton()
    {
        //没有游戏结束才能进行验证
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
                    TipsText.text = "该数范围在" + RemainData.Min.ToString() + "-" + RemainData.Max.ToString() + "之间";
                    EventManager.CallWrongAnswer();
                    ResultText.text = "你猜错了";
                    ClickTimes = 0;
                    Num = 0;
                    InputText.text = Num.ToString();
                }
            }
           
            RemainTimesText.text = "剩余次数：" + RemainData.Times.ToString() + "次";
        }
    }
    //比较数的大小并且进行改变
    private void CompareBigger(int num)
    {
        //玩家输入的数字必须小于等于当前提示最大值才能进行有效的提示
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
    //返回按钮被单击的事件
    public void OnReturnButtonClick()
    {
        //只有不是游戏结束才可以打开返回按钮
        if (IsGameOver==false)
        {
            PausingPanel.SetActive(true);
        }
    }

    //OnEnable和OnDisable方法用来注册和注销事件
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

    //游戏结束事件,并且保存游戏数据
    private void OnGameOver()
    {
        GameOverPanel.SetActive(true);
        if (isSuccessful)
        {
            InformationText.text = "猜数结果：成功\n\n猜数次数：" + (SpareTimes - RemainData.Times).ToString() + "次\n\n最终答案："+RandNumber.ToString();
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
            InformationText.text = "猜数结果：失败\n\n猜数次数：" + (SpareTimes - RemainData.Times).ToString() + "次\n\n最终答案："+RandNumber.ToString();
        } 
    }

    //初始化游戏的随机值数据
    public void InitialGame(ConstantData.Difficulty difficulty, ConstantData.Model model)
    {
        /*
         * 根据游戏的模式来初始化变量
         * 如果是无限次模式，就初始化为0，
         * 否则就根据游戏难度来初始化变量的值
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

    //切换到主场景
    public void SwitchToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    //重新开始游戏
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    //额外的提示
    public void ExtraTips()
    {
        if (RemainData.Times-2<=0)
        {
            EventManager.CallWrongAnswer();
            ResultText.text = "猜数次数不足！";
        }
        else
        {
            RemainData.Times -= 2;
            RemainTimesText.text = "剩余次数：" + RemainData.Times.ToString() + "次";
            string[] result = { "质数", "合数","奇数","偶数" };
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
                    ExtraTipsText.text="它是一个质数";
                }
                else
                {
                    ExtraTipsText.text = "它不是一个质数";
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
                    ExtraTipsText.text = "它是一个合数";
                }
                else
                {
                    ExtraTipsText.text = "它不是一个合数";
                }
            }
            else if (index == 2||index==3)
            {
                if (RandNumber%2==0)
                {
                    ExtraTipsText.text = "它是一个偶数";
                }
                else
                {
                    ExtraTipsText.text = "它是一个奇数";
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
