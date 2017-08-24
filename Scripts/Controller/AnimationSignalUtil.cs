using System;
using System.Collections;
using UnityEngine;

public enum AnimationHitType
{
    firstHit = 0,
    otherHit = 1,
    lastHit = 2,
}

public class AnimationSignalUtil
{
	private static AnimationSignalUtil instance_;
	private Hashtable initializedAniClips = new Hashtable();
	public static AnimationSignalUtil instance
	{
		get
		{
			if (AnimationSignalUtil.instance_ == null)
			{
				AnimationSignalUtil.instance_ = new AnimationSignalUtil();
			}
			return AnimationSignalUtil.instance_;
		}
	}
	public void AddSignals(AnimationClip clip, AnimationSignalData data)
	{
		if (null == clip)
		{
			return;
		}
		string[] separator = new string[]
		{
			","
		};
		if (this.initializedAniClips.ContainsKey(clip))
		{
			return;
		}
		string[] array = data.signalFrames.Split(separator, StringSplitOptions.RemoveEmptyEntries);
		string[] array2 = data.signalParam.Split(separator, StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < array.Length; i++)
		{
			float time = Convert.ToSingle(array[i]) / 30f;
			AnimationEvent animationEvent = new AnimationEvent();
			animationEvent.time = time;
			if (data.signalParam.Length > 0)
			{
				if (array2.Length > 1)
				{
					animationEvent.stringParameter = array2[i];
				}
				else
				{
					animationEvent.stringParameter = data.signalParam;
				}
			}
			else
			{
				animationEvent.stringParameter = data.aniName;
			}
			animationEvent.functionName = "OnAnimationEvent";
			animationEvent.intParameter = (int)data.signalType;
            if (i == 0)//第一次伤害
            {
                animationEvent.floatParameter = 0;
            }
            else if (i == array.Length - 1)//最后一次伤害
            {
                animationEvent.floatParameter = 2;
            }
            else//中间的
            {
                animationEvent.floatParameter = 1;
            }
            //animationEvent.floatParameter = ((i != array.Length - 1) ? 0f : 1f);
			animationEvent.messageOptions = SendMessageOptions.DontRequireReceiver;
			clip.AddEvent(animationEvent);
		}
	}
	public void SignalAdded(AnimationClip clip)
	{
		if (null == clip)
		{
			return;
		}
		AnimationSignalUtil.instance_.initializedAniClips[clip] = true;
	}
}
