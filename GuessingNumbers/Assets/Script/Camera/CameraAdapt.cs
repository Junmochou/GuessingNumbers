using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdapt : MonoBehaviour
{
    //屏幕高度与屏幕高度的比例
    private float rate;
    //这个是相机基准渲染尺寸
    public float zoomBase;
    // Start is called before the first frame update
    void Start()
    {
        rate = (float)Screen.height / (float)Screen.width;
        Camera.main.orthographicSize = zoomBase * rate * 0.5f;
    }

}
