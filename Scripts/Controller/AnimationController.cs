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
    /// ���Ŷ������ƣ���isSkillΪTrueʱ�����㶯�����ŵ��ٶȣ��������Ч��
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
    /// ���Ŷ�������
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
    /// �������ѭ������
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="isSkill"></param>
    public void PlayWithCheckLoop(string animation, bool isSkill = false)
    {
        //����˶����ڲ��ţ��򷵻�
        if (this._animation.IsPlaying(animation))
        {
            return;
        }
        //ȡ��������Ϸ����ļ���
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
    /// ֹͣ��ҽ�ɫ�����еĶ���������ȡ������
    /// </summary>
    public void Stop()
    {
        this._animation.Stop();
        //this._skillMgr.CancelSkill();
    }
    /// <summary>
    /// �жϵ�ǰ�����Ƿ��ڲ���
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
    /// ʵ�ֲ�����Ч�Ĺ���
    /// </summary>
    /// <param name="signalParam">��Ч������</param>
    public void SendEffectSignal(string signalParam)
    {
    //    if (_effectCtrl == null) return;//�򲹶�
   //     this._effectCtrl.OnSignal(signalParam, true);
    }
    /// <summary>
    /// ʵ��ֹͣ������Ч�Ĺ���
    /// </summary>
    /// <param name="signalParam">��Ч������</param>
    public void SendEffectStopSignal(string signalParam)
    {
    //    this._effectCtrl.OnSignal(signalParam, false);
    }
    /// <summary>
    /// ���ͼ��ܲ�����ʵ�ּ�����Ч����
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
    /// ʵ�ֲ�����Ƶ�Ĺ���
    /// </summary>
    /// <param name="signalParam">��Ƶ������</param>
    public void SendSoundSignal(string signalParam)
    {
        //if (null != this._soundCtrl)
        //{
        //    this._soundCtrl.OnSignal(signalParam);
        //}
    }
    /// <summary>
    /// ��ö�����ʼ��ֵ
    /// </summary>
    /// <param name="animation">��������</param>
    /// <param name="signalType">�ź�����</param>
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
    /// ֹͣ������Ч
    /// </summary>
    public void StopAllEffects()
    {
    //    this._effectCtrl.StopAllEffects();
    }

    /// <summary>
    /// �������ö���
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
    /// ���ű��ܶ���
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
    /// ͨ��aniname����signalparam����
    /// </summary>
    /// <param name="aniName">������</param>
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
    /// ���ų�̶���
    /// </summary>
    public void PlayDash()
    {

    }

}
