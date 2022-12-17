using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 该类存储了玩家的所有存档数据
/// </summary>
[System.Serializable]
public class GameDataManager : MonoBehaviour
{
    //ST就是simple Times
    public static int LimitST = 0;
    //SJ就是积累的猜数次数
    public static int LimitSJ = 0;

    public static int LimitNT = 0;
    public static int LimitNJ = 0;

    public static int LimitDT = 0;
    public static int LimitDJ = 0;

    public static int LimitlessST = 0;
    public static int LimitlessSJ = 0;

    public static int LimitlessNT = 0;
    public static int LimitlessNJ = 0;

    public static int LimitlessDT = 0;
    public static int LimitlessDJ = 0;

    public void SaveData(ConstantData.Difficulty difficulty ,ConstantData.Model model ,int times)
    {
        if (difficulty==ConstantData.Difficulty.Simple &&model==ConstantData.Model.Limit)
        {
            LimitST += 1;
            LimitSJ += times;
        }else if (difficulty == ConstantData.Difficulty.Normal && model == ConstantData.Model.Limit)
        {
            LimitNT += 1;
            LimitNJ += times;
        }else if (difficulty == ConstantData.Difficulty.Difficult && model == ConstantData.Model.Limit)
        {
            LimitDT += 1;
            LimitDJ += times;
        }else if (difficulty == ConstantData.Difficulty.Simple && model == ConstantData.Model.Limitless)
        {
            LimitlessST += 1;
            LimitlessSJ += times;
        }else if (difficulty == ConstantData.Difficulty.Normal && model == ConstantData.Model.Limitless)
        {
            LimitlessNT += 1;
            LimitlessNJ += times;
        }else if (difficulty == ConstantData.Difficulty.Difficult && model == ConstantData.Model.Limitless)
        {
            LimitlessDT += 1;
            LimitlessDJ += times;
        }

        string Data = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("GameData",Data);
        PlayerPrefs.Save();
    }

}