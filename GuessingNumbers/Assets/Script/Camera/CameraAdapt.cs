using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdapt : MonoBehaviour
{
    //��Ļ�߶�����Ļ�߶ȵı���
    private float rate;
    //����������׼��Ⱦ�ߴ�
    public float zoomBase;
    // Start is called before the first frame update
    void Start()
    {
        rate = (float)Screen.height / (float)Screen.width;
        Camera.main.orthographicSize = zoomBase * rate * 0.5f;
    }

}
