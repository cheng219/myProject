//=====================
//作者：邓成
//日期：2017/8/12
//用途：场景中存在的角色的基类
//=====================

using UnityEngine;
using System.Collections;

public class Actor : FSMBase 
{
    protected bool isDead = false;
    /// <summary>
    /// 是否已经死亡
    /// </summary>
    public bool IsDead { get { return isDead; } }

    /// <summary>
    /// 当前无buff状态下的移动速度
    /// </summary>
    protected float curMoveSpeed = 6.0f;
    /// <summary>
    /// 特效控制器
    /// </summary>
    public FxCtrl fxCtrl = null;
    /// <summary>
    /// 动作控制器
    /// </summary>
    [HideInInspector]
    public AnimationController aniCtrl;
    /// <summary>
    /// 移动控制器
    /// </summary>
    [HideInInspector]
    public NavMeshAgent navAgent;
    /// <summary>
    /// 音效控制器
    /// </summary>
    [HideInInspector]
    public AudioSource audioSource;
    /// <summary>
    /// 尽量避免使用Awake等unity控制流程的接口来初始化，而改用自己调用的接口
    /// </summary>
    protected virtual void Init()
    {
        fxCtrl = gameObject.GetComponent<FxCtrl>();
        if (fxCtrl == null) fxCtrl = gameObject.AddComponent<FxCtrl>();
        Regist();
    }

    protected void OnDestroy()
    {
        UnRegist();
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    protected virtual void Regist()
    {

    }
    /// <summary>
    /// 注销事件
    /// </summary>
    public virtual void UnRegist()
    {

    }
    protected override void InitStateMachine()
    {
        base.InitStateMachine();
        Init();
    }
}
