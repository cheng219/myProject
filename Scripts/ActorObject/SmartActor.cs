//=====================
//作者：邓成
//日期：2017/8/12
//用途：怪物及英雄的基类
//=====================

using UnityEngine;
using System.Collections;

public class SmartActor : Actor 
{
    protected ActorInfo actorInfo;



    /// <summary>
    /// 当前锁定目标
    /// </summary>
    public Actor curTarget = null;



    public virtual void SetComponent()
    {

    }

    protected override void InitStateMachine()
    {
        base.InitStateMachine();
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
