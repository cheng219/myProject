using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mahjong
{
    public int id;
    /// <summary>
    /// 表里面的ID,最大四个Mahjong的type相同
    /// </summary>
    public int type;

    public MahjongColor mahjongColor;

    public string name;

    public string serializeName;

    public static int CompareMahjong(Mahjong mahjong1, Mahjong mahjong2)
    {
        if (mahjong1.type > mahjong2.type)
            return -1;
        if (mahjong1.type > mahjong2.type)
            return 1;
        return 0;
    }

    public string NameIndex
    {
        get
        {
            return serializeName.Substring(serializeName.Length - 1,1);
        }
    }
}
public enum MahjongColor
{ 
    筒子,
    条子,
    万子,
}
