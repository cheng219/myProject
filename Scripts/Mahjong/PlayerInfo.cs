using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class PlayerInfo
{
    //         3
    //    4        2
    //         1
    public int playerPositionCode = 0;

    public System.Action OnOwnMahjongListUpdateEvent;
    public System.Action OnEatMahjongListUpdateEvent;
    public System.Action OnBumpMahjongListUpdateEvent;
    /// <summary>
    /// 手里的
    /// </summary>
    public List<Mahjong> ownMahjongList = new List<Mahjong>();
    /// <summary>
    /// 吃到的
    /// </summary>
    public List<Mahjong> eatMahjongList = new List<Mahjong>();
    /// <summary>
    /// 碰到的
    /// </summary>
    public List<Mahjong> bumpMahjongList = new List<Mahjong>();

    protected PlayerPosition GetPlayerPosition(PlayerInfo playerInfo)
    {
        int diffPos = playerInfo.playerPositionCode - playerPositionCode;
        if (Mathf.Abs(diffPos) == 2)
        {
            return PlayerPosition.IsOppositeOne;
        }
        if (diffPos == 1 || diffPos == -3)
        {
            return PlayerPosition.IsRightOne;
        }
        if (diffPos == -1 || diffPos == 3)
        {
            return PlayerPosition.IsLeftOne;
        }
        Debug.LogError("奇怪的位置:" + diffPos);
        return PlayerPosition.None;
    }

    public void CheckNewMahjong(Mahjong newOne,PlayerInfo playerInfo = null)
    {
        if (playerInfo != null)
        {
            PlayerPosition playerPos = GetPlayerPosition(playerInfo);
            CheckAbandonOne(newOne, playerPos);
        }
        else
        {
            CheckCatchOne(newOne);
        }
    }

    protected void CheckAbandonOne(Mahjong newOne, PlayerPosition playerPos)
    {
       
    }

    protected void CheckCatchOne(Mahjong newOne)
    { 
        
    }

    bool Bump(Mahjong _mahjong)
    {
        int type = _mahjong.type;
        int haveCount = 0;
        for (int i = 0, length = ownMahjongList.Count; i < length; i++)
        {
            if (ownMahjongList[i].type == type)
            {
                haveCount++;
                if (haveCount == 2) return true;
            }
        }
        return false;
    }
    bool Eat(Mahjong _mahjong)
    {
        List<Mahjong> list = GetSamaColorList(_mahjong);
        list.Add(_mahjong);
        string serializeList = SerializeList(list);
        string[] eatSerialize = MahjongConst.eatSerialize;
        int containsCount = 0;
        for (int i = 0,length=eatSerialize.Length; i < length; i++)
        {
            char[] chars = eatSerialize[i].ToCharArray();
            for (int j = 0, lengthJ = chars.Length; j < lengthJ; j++)
            {
                if (!serializeList.Contains(chars[i].ToString()))
                {
                    break;
                }
                else
                {
                    containsCount++;
                    if (containsCount == lengthJ) return true;
                }
            }
        }
        return false;
    }

    List<Mahjong> GetSamaColorList(Mahjong _mahjong)
    {
        List<Mahjong> list = new List<Mahjong>();
        for (int i = 0,length=ownMahjongList.Count; i < length; i++)
        {
            if (ownMahjongList[i].mahjongColor == _mahjong.mahjongColor)
                list.Add(ownMahjongList[i]);
        }
        return list;
    }

    string SerializeList(List<Mahjong> list)
    {
        
        StringBuilder builder = new StringBuilder();
        for (int i = 0, length = list.Count; i < length; i++)
        {
            builder.Append(list[i].NameIndex);
        }
        return builder.ToString();
    }

    public void SortOwnList()
    {
        ownMahjongList.Sort(Mahjong.CompareMahjong);
        if (OnOwnMahjongListUpdateEvent != null)
            OnOwnMahjongListUpdateEvent();
    }
    public void SortEatList()
    {
        eatMahjongList.Sort(Mahjong.CompareMahjong);
        if (OnEatMahjongListUpdateEvent != null)
            OnEatMahjongListUpdateEvent();
    }
    public void SortBumpList()
    {
        bumpMahjongList.Sort(Mahjong.CompareMahjong);
        if (OnBumpMahjongListUpdateEvent != null)
            OnBumpMahjongListUpdateEvent();
    }
}

public enum PlayerPosition
{ 
    None,
    IsLeftOne,
    IsRightOne,
    IsOppositeOne,
}
public enum PlayerButton
{
    胡,
    吃,
    碰,
    杠,
}
