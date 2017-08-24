//=====================
//作者：邓成
//日期：2017/8/12
//用途：英雄的数据类
//=====================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBaseInfo : SmartActorInfo
{

    #region 英雄独有的属性
    /// <summary>
    /// 承受伤害技能点  
    /// </summary>
    public float harmSkillRatio;
    /// <summary>
    /// 英雄职业
    /// </summary>
    public int heroCareer;
    /// <summary>
    /// 英雄阵容
    /// </summary>
    public int heroCamp;
    #endregion

    public override void InitAttr(Dictionary<string, string> roleData)
    {
        base.InitAttr(roleData);
        //if (roleData.ContainsKey(RoleKey.harmSkillRatio.ToString()))
        //{
        //    harmSkillRatio = float.Parse(roleData[RoleKey.harmSkillRatio + ""]);
        //}
        //if (roleData.ContainsKey(RoleKey.career.ToString()))
        //{
        //    heroCareer = int.Parse(roleData[RoleKey.career + ""]);
        //}
        //if (roleData.ContainsKey(RoleKey.camp.ToString()))
        //{
        //    heroCamp = int.Parse(roleData[RoleKey.camp + ""]);
        //}
    }
}
