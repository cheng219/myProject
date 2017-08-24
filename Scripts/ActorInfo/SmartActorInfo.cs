//=====================
//作者：邓成
//日期：2017/8/12
//用途：怪物及英雄的数据类
//=====================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class SmartActorInfo : ActorInfo
{
    public string modelName;


    public virtual void InitAttr(Dictionary<string, string> roleData)
    {
 
    }
}
