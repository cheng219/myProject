using System;
using UnityEngine;
using System.Collections.Generic;

public class AnimationController : MonoBehaviour
{
    public AnimationSignalData[] signalDataList;
    public int PositionCode = 1;
    public Animation _animation;
    private AnimationState _curState;
    [HideInInspector]
    private string queuedAnimation;

    public SkinnedMeshRenderer[] skinnedMeshes;




    [HideInInspector]
    public List<string> normalAttackAnimations = new List<string>();
    private void Start()
    {

    }
    void Awake()
    {

    }

    void OnDestroy()
    {
    }


    //private void Awake()
    public void AwakeByAI()
    {
        //_animation = base.transform.gameObject.GetComponent<Animation>();
        //_animation = animation;
        for (int i = 0; i < this.signalDataList.Length; i++)
        {
            AnimationClip clip = this._animation.GetClip(this.signalDataList[i].aniName);
            AnimationSignalUtil.instance.AddSignals(clip, this.signalDataList[i]);
        }

        for (int j = 0; j < this.signalDataList.Length; j++)
        {
            AnimationClip clip2 = this._animation.GetClip(this.signalDataList[j].aniName);
            AnimationSignalUtil.instance.SignalAdded(clip2);

            if (clip2 && clip2.name.IndexOf("attack") == 0)
            {
                normalAttackAnimations.Add(clip2.name);
            }
        }
        skinnedMeshes = GetComponentsInChildren<SkinnedMeshRenderer>();
    }
    private void OnAnimationEvent(AnimationEvent evt)
    {
        switch (evt.intParameter)
        {
            case 0:
                this.SendEffectSignal(evt.stringParameter);
                break;
            case 1:
                this.SendSkillSignal(evt.stringParameter,  (AnimationHitType) evt.floatParameter);
                break;
            case 2:
                this.SendSoundSignal(evt.stringParameter);
                break;
        }
    }
    /// <summary>
    /// 播放动画控制，当isSkill为True时，计算动画播放的速度，以配合特效。
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="isSkill"></param>
    public void Play(string animation, bool isSkill = false, bool isCrossFade = true)
    {
        if (this._animation.IsPlaying(animation))
        {
            return;
        }
        if (this._animation[animation] == null)
        {
            return;
        }
        if (this._animation[animation].speed == 0)
        {
            return;
        }

        //this._skillMgr.CancelSkill();

        if (this._curState!=null&& this._curState.speed == 0)
        {
            return;
        }
        //this._animation.Play(animation);
        if (isCrossFade)
        {
            this._animation.CrossFade(animation, 0.2f);
        }
        else
        {
            this._animation.Play(animation);
        }
        this._animation.Rewind();
        this._curState = this._animation[animation];
        float num = this.GetAnimationSpeed(animation);
        this._curState.speed = num;
    }

    float GetAnimationSpeed(string animName)
    {
        return 1f;
    }

    /// <summary>
    /// 播放动画队列
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="isSkill"></param>
    public void PlayQueued(string animation, bool isSkill = false)
    {
        if (this._animation.IsPlaying(animation))
        {
            return;
        }
        //this._skillMgr.CancelSkill();
        this._animation.PlayQueued(animation, QueueMode.CompleteOthers, PlayMode.StopAll);
        this.queuedAnimation = animation;
    }

    /// <summary>
    /// 动画检测循环播放
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="isSkill"></param>
    public void PlayWithCheckLoop(string animation, bool isSkill = false)
    {
        //如果此动画在播放，则返回
        if (this._animation.IsPlaying(animation))
        {
            return;
        }
        //取消所有游戏对象的技能
        //this._skillMgr.CancelSkill();
        if (this._animation.wrapMode == WrapMode.Loop || (this._curState != null && this._curState.wrapMode == WrapMode.Loop))
        {
            this._animation.Play(animation);
            this._animation.Rewind();
            this._curState = this._animation[animation];
            float num = this.GetAnimationSpeed(animation);
            this._curState.speed = num;
        }
        else
        {
            this._animation.PlayQueued(animation, QueueMode.CompleteOthers, PlayMode.StopAll);
            this.queuedAnimation = animation;
        }
    }

    private void Update()
    {
        if (this.queuedAnimation != null && this.IsPlaying(this.queuedAnimation))
        {
            this._curState = this._animation[this.queuedAnimation];
            this.queuedAnimation = null;
        }
    }

    /// <summary>
    /// 停止玩家角色的所有的动画，并且取消技能
    /// </summary>
    public void Stop()
    {
        this._animation.Stop();
        //this._skillMgr.CancelSkill();
    }
    /// <summary>
    /// 判断当前动画是否在播放
    /// </summary>
    /// <param name="animation"></param>
    /// <returns></returns>
    public bool IsPlaying(string animation = null)
    {
        if (null == this._animation)
        {
            return false;
        }
        if (animation == null)
        {
            return this._animation.isPlaying;
        }
        return this._animation.IsPlaying(animation);
    }
    public bool IsExist(string animation)
    {
        return this._animation[animation] != null;
    }
    /// <summary>
    /// 实现播放特效的功能
    /// </summary>
    /// <param name="signalParam">特效的名称</param>
    public void SendEffectSignal(string signalParam)
    {
    //    if (_effectCtrl == null) return;//打补丁
   //     this._effectCtrl.OnSignal(signalParam, true);
    }
    /// <summary>
    /// 实现停止播放特效的功能
    /// </summary>
    /// <param name="signalParam">特效的名称</param>
    public void SendEffectStopSignal(string signalParam)
    {
    //    this._effectCtrl.OnSignal(signalParam, false);
    }
    /// <summary>
    /// 发送技能参数，实现技能特效调用
    /// </summary>
    /// <param name="signalParam"></param>
    /// <param name="isLastHit"></param>
    public void SendSkillSignal(string signalParam, AnimationHitType animationHitType)
    {
        //if (!LevelManager.IsExist())
        //{
        //    return;
        //}
        //this._skillMgr.OnSignal(signalParam, animationHitType);
    }
    /// <summary>
    /// 实现播放音频的功能
    /// </summary>
    /// <param name="signalParam">音频的名称</param>
    public void SendSoundSignal(string signalParam)
    {
        //if (null != this._soundCtrl)
        //{
        //    this._soundCtrl.OnSignal(signalParam);
        //}
    }
    /// <summary>
    /// 获得动画开始桢值
    /// </summary>
    /// <param name="animation">动画名称</param>
    /// <param name="signalType">信号类型</param>
    /// <returns></returns>
    //public float GetFirstSignalFrame(string animation, SignalType signalType)
    //{
    //    for (int i = 0; i < this.signalDataList.Length; i++)
    //    {
    //        if (this.signalDataList[i].signalFrames.Length != 0)
    //        {
    //            if (this.signalDataList[i].signalType == signalType && animation == this.signalDataList[i].aniName)
    //            {
    //                string[] separator = new string[]
    //                {
    //                    ","
    //                };
    //                string[] array = this.signalDataList[i].signalFrames.Split(separator, StringSplitOptions.RemoveEmptyEntries);
    //                return Convert.ToSingle(array[0]) / 30f;
    //            }
    //        }
    //    }
    //    return 0f;
    //}
    public float GetAnimationLength(string animation)
    {
        AnimationClip clip = this._animation.GetClip(animation);
        if (null == clip)
        {
            return 0f;
        }
        return clip.length * this.GetAnimationSpeed(animation);
    }

    /// <summary>
    /// 停止所有特效
    /// </summary>
    public void StopAllEffects()
    {
    //    this._effectCtrl.StopAllEffects();
    }

    /// <summary>
    /// 播放闲置动画
    /// </summary>
    public void PlayIdle()
    {
        if (!IsPlaying("idle"))
        {
            //if (_ai.GetRootGameObject().name == "Player3")
            //{
            //    Debuger.Log("1111");
            //}
            _animation["idle"].wrapMode = WrapMode.Loop;
            if (IsPlaying("run"))
            {
                Play("idle", false);
            }
            else
            {
                Play("idle", false, false);
            }
        }
    }

    /// <summary>
    /// 播放奔跑动画
    /// </summary>
    public void PlayRun()
    {
        if (_animation["run"] == null)
        {
            return;
        }

        if (!IsPlaying("run"))
        {
            _animation["run"].wrapMode = WrapMode.Loop;
            if (IsPlaying("idle"))
            {
                Play("run", false);
            }
            else
            {
                Play("run", false, false);
            }
        }
    }

    /// <summary>
    /// 通过aniname查找signalparam参数
    /// </summary>
    /// <param name="aniName">动作名</param>
    /// <returns></returns>
    public string FindSignalParamByAniName(string aniName)
    {
        for (int i = 0; i < signalDataList.Length; i++)
        {
            //if (signalDataList[i].signalType == SignalType.Skill)
            //{
            //    if (aniName == signalDataList[i].aniName)
            //    {
            //        if (string.IsNullOrEmpty(signalDataList[i].signalParam))
            //            return aniName;
            //        return signalDataList[i].signalParam;
            //    }
            //}
        }
        return aniName;
    }

    /// <summary>
    /// 播放冲刺动画
    /// </summary>
    public void PlayDash()
    {

    }

}
