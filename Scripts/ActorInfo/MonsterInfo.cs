//=====================
//作者：邓成
//日期：2017/8/12
//用途：怪物的数据类
//=====================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class MonsterInfo : SmartActorInfo {

    #region 怪物独有的属性
    /// <summary>
    /// 自身魔能点
    /// </summary>
    public float magicPower = 0f;
    //魔法免疫
    public bool isPhysicImmune = false;
    //物理免疫
    public bool isMagicImuune = false;
     //怪物高度
    public float height;
    /// <summary>
    /// 怪物质量
    /// </summary>
    public float mass = 3f;
    /// <summary>
    /// 怪物大小
    /// </summary>
    public float scale = 1f;
    //是否被打断，播放被打击动画。
    //public BeHittedType IsHit;
    //普通攻击间隔时间
    public float atkDelay;
    //攻击之前是否后退
    public bool atkBack;
    //
    public float MaxProtectHp = 0f;
    //
    public float ProtectHpRecoverSpeed = 0f;
    //免疫buff类型
    //public ImmunityBuffType[] immunityBuffType;
    /// <summary>
    /// 是否显示魔晶
    /// </summary>
    [HideInInspector]
    public bool IsShowMagicPower = true;
    #endregion
    /// <summary>
    /// 根据静态表配置构造一个数据类
    /// </summary>
    public MonsterInfo(Dictionary<string, string> roleData)
    {
        InitAttr(roleData);
    }
    public override void InitAttr(Dictionary<string, string> roleData)
    {
        base.InitAttr(roleData);
        //if (roleData.ContainsKey(MonsterKey.model.ToString()))
        //{
        //    modelName = roleData[MonsterKey.model.ToString()];
        //}
        //if(roleData.ContainsKey(MonsterKey.MonsterMaxMagicPower.ToString()))
        //{
        //    float.TryParse(roleData[MonsterKey.MonsterMaxMagicPower.ToString()], out magicPower);
        //}
        ////怪物免疫属性判断
        //if (roleData.ContainsKey(MonsterKey.immueType.ToString()))
        //{
        //    //怪物设置免疫类型
        //    string immueType = roleData[MonsterKey.immueType.ToString()];
        //    if (immueType == "1")
        //    {
        //        isPhysicImmune = true;
        //        isMagicImuune = false;
        //    }
        //    else if (immueType == "2")
        //    {
        //        isPhysicImmune = false;
        //        isMagicImuune = true;
        //    }
        //    else if (immueType == "3")
        //    {
        //        isPhysicImmune = true;
        //        isMagicImuune = true;
        //    }
        //    else
        //    {
        //        isPhysicImmune = false;
        //        isMagicImuune = false;
        //    }
        //    if (roleData.ContainsKey(MonsterKey.height.ToString()))
        //    {
        //        height = float.Parse(roleData[MonsterKey.height.ToString()]);
        //    }
        //    if (roleData.ContainsKey(MonsterKey.scale.ToString()))
        //    {
        //        scale = float.Parse(roleData[MonsterKey.scale.ToString()]);
        //    }
        //    if (roleData.ContainsKey(MonsterKey.mass.ToString()))
        //    {
        //        mass = float.Parse(roleData[MonsterKey.mass.ToString()]);
        //    }
        //    if (roleData.ContainsKey(MonsterKey.isHit.ToString()))
        //    {
        //        //是否被打打击(BeHittedType)int.Parse(roleData[MonsterKey.isHit.ToString()])
        //        IsHit = GetBeHittedTypeByNumber(roleData[MonsterKey.isHit.ToString()]);
        //    }
        //    if (roleData.ContainsKey(MonsterKey.atkDelay.ToString()))
        //    {
        //        //打击延时
        //        atkDelay = float.Parse(roleData[MonsterKey.atkDelay.ToString()]);
        //    }
        //    if (roleData.ContainsKey(MonsterKey.atkBack.ToString()))
        //    {
        //        //是否后退
        //        atkBack = roleData[MonsterKey.atkBack.ToString()] == "1";
        //    }
        //    if (roleData.ContainsKey(MonsterKey.protectHp.ToString()))
        //    {
        //        float.TryParse(roleData[MonsterKey.protectHp.ToString()], out  MaxProtectHp);
        //    }
        //    if (roleData.ContainsKey(MonsterKey.protectHpRecover.ToString()))
        //    {
        //        float.TryParse(roleData[MonsterKey.protectHpRecover.ToString()], out  ProtectHpRecoverSpeed);
        //    }
        //    if (roleData.ContainsKey(MonsterKey.immunityBuffType.ToString()))
        //    {
        //        string immuBuffStr = roleData[MonsterKey.immunityBuffType.ToString()];
        //        if (!string.IsNullOrEmpty(immuBuffStr) && immuBuffStr != "0")
        //        {
        //            string[] immuBuffs = immuBuffStr.Split(',');
        //            immunityBuffType = new ImmunityBuffType[immuBuffs.Length];
        //            for (int i = 0; i < immuBuffs.Length; i++)
        //            {
        //                int index = 0;
        //                if (int.TryParse(immuBuffs[i], out index))
        //                {
        //                    immunityBuffType[i] = (ImmunityBuffType)index;
        //                }
        //                else
        //                {
        //                    Debuger.LogError("护盾免疫类型数据错误:" + immuBuffStr);
        //                    return;
        //                }
        //            }
        //        }
        //    }
        //}
    }
    //private BeHittedType GetBeHittedTypeByNumber(string Numbers)
    //{
    //    BeHittedType bht = BeHittedType.None;
    //    switch (Numbers)
    //    { 
    //        case "0":
    //            bht = BeHittedType.NormalAndKnockBack;
    //            break;
    //        case "1":
    //            bht = BeHittedType.None;
    //            break;
    //        case "2":
    //            bht = BeHittedType.KnockBack;
    //            break;
    //        case "3":
    //            bht = BeHittedType.Normal;
    //            break;
    //    }

    //    return bht;
    
    //}
}
