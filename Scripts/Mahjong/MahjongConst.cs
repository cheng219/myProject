using UnityEngine;
using System.Collections;

public class MahjongConst
{
    public static int TotalMahjongCount = 108;
    /// <summary>
    /// 每个玩家最多牌数
    /// </summary>
    public static int MaxMahjongCountPerMan = 13;
    /// <summary>
    /// 堆砌的叠数A
    /// </summary>
    public static int PileUpAMahjongCount = 13;
    /// <summary>
    /// 堆砌的叠数B
    /// </summary>
    public static int PileUpBMahjongCount = 14;
    /// <summary>
    /// 每次抓的牌数
    /// </summary>
    public static int EveryTimeCatchCount = 4;
    public static int PlayerCount = 4;

    public static string[] eatSerialize = { "123", "234", "345", "456", "567", "678", "789" };
}
