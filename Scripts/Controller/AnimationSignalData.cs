using System;
[Serializable]
public class AnimationSignalData
{
	public string aniName;
	public string signalFrames;
	public string signalParam;
	public SignalType signalType;
}

public enum SignalType
{
    Effect,
    Skill,
    Sound
}
