using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPanelFuction : MonoBehaviour
{
    //基础面板的游戏对象
    public GameObject BasicPanel;

    //关闭基础面板
    public void CloseBasicPanel()
    {
        BasicPanel.SetActive(false);
    }
}
