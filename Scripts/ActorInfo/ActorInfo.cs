//=====================
//作者：邓成
//日期：2017/8/12
//用途：场景中存在的角色的数据类
//=====================

using UnityEngine;
using System.Collections;

[System.Serializable]
public class ActorInfo 
{
    /// <summary>
    /// 表中的配置ID
    /// </summary>
    public int EID = 0;
    /// <summary>
    /// 名字
    /// </summary>
    public string name = string.Empty;
    /// <summary>
    /// 是否无敌
    /// </summary>
    [HideInInspector]
    public bool IsInvincible = false;
    /// <summary>
    /// 是否沉默
    /// </summary>
    [HideInInspector]
    public bool IsSilence = false;
    /// <summary>
    /// 是否缴械
    /// </summary>
    [HideInInspector]
    public bool IsDisarm = false;
}
