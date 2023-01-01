using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �������ڹ���ͳ�Ƴ���
/// </summary>
public class ConclusionSceneManager : MonoBehaviour
{
    //�б���Ŀ����Ϸ���󼯺�
    public List<ItemManager> ItemManagerList = new List<ItemManager>();

    

    private void Start()
    {
            LoadConclusionListView();
    }
    /// <summary>
    /// ���ص���Ϸ������
    /// </summary>
    public void OnCloseButtonClick()
    {
        SceneManager.LoadScene("MainScene");
        LoadConclusionListView();
    }

    //�����б�
    public void LoadConclusionListView()
    {
        for (int i=0;i<ItemManagerList.Count;i++)
        {
            ItemManagerList[i].FinishAndTotalText.text = "�����ɹ�����:" + PlayerPrefs.GetInt(ConstantData.DataName[i]).ToString();
            ItemManagerList[i].AverTimesText.text = "����ƽ������:" + (PlayerPrefs.GetFloat(ConstantData.DataName[i] + "1") / (float)PlayerPrefs.GetInt(ConstantData.DataName[i])).ToString();
        }
    }
}
    
