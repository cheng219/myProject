//=====================
//作者：邓成
//日期：2017/7/26
//用途：一些状态机的基本定义，为状态机的逻辑实现服务。
//=====================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSMBase : MonoBehaviour
{

    [System.NonSerialized]
    public fsm.Machine stateMachine;


    protected void Awake()
    {
        stateMachine = new fsm.Machine();
        InitStateMachine();
    }

    public virtual void Reset()
    {
        if (stateMachine != null)
            stateMachine.Restart();
    }

    protected virtual void InitStateMachine() { }
}

