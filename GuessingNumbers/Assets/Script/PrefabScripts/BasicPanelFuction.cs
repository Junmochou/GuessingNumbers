using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPanelFuction : MonoBehaviour
{
    //����������Ϸ����
    public GameObject BasicPanel;

    //�رջ������
    public void CloseBasicPanel()
    {
        BasicPanel.SetActive(false);
    }
}
