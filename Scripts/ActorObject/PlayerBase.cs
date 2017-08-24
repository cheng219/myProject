//=====================
//作者：邓成
//日期：2017/8/12
//用途：英雄的表现类
//=====================

using UnityEngine;
using System.Collections;

public class PlayerBase : SmartActor {

    public static PlayerBase CreateNew()
    {
        return null;
    }

    protected override void Init()
    {
        base.Init();
    }
    protected override void Regist()
    {
        base.Regist();
    }

    public override void UnRegist()
    {
        base.UnRegist();
    }
}
