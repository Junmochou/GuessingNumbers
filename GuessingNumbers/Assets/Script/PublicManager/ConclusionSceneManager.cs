using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 该类用于管理统计场景
/// </summary>
public class ConclusionSceneManager : MonoBehaviour
{
    //列表项目的游戏对象集合
    public List<ItemManager> ItemManagerList = new List<ItemManager>();

    

    private void Start()
    {
            LoadConclusionListView();
    }
    /// <summary>
    /// 返回到游戏主界面
    /// </summary>
    public void OnCloseButtonClick()
    {
        SceneManager.LoadScene("MainScene");
        LoadConclusionListView();
    }

    //加载列表
    public void LoadConclusionListView()
    {
        for (int i=0;i<ItemManagerList.Count;i++)
        {
            ItemManagerList[i].FinishAndTotalText.text = "猜数成功场次:" + PlayerPrefs.GetInt(ConstantData.DataName[i]).ToString();
            ItemManagerList[i].AverTimesText.text = "猜数平均次数:" + (PlayerPrefs.GetFloat(ConstantData.DataName[i] + "1") / (float)PlayerPrefs.GetInt(ConstantData.DataName[i])).ToString();
        }
    }
}
    
