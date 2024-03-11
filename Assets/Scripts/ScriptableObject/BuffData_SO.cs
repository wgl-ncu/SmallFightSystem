using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Battle Config", menuName = "DataCreate/Buff/New Buff")]
public class BuffData_SO : ScriptableObject
{
    public int id;
    [Header("buff作用目标")] public BuffTarget target;
    public int targetNum;
    [Header("buff范围")]public float range;
    [Header("基础数值")] public float buffValue;
    [Header("计算方式")] public ValueCalWay calWay;
    [Header("buff效果")] public BuffEffect effect;
    [Header("改变的属性，仅在effect为ChangeProp时生效")] public LogicUnitProp prop;
    [Header("生效方式")] public BuffTakeEffectWay takeEffectWay;
    [Header("生效间隔，仅在takeEffectWay为BaseTime时生效")] public float takeEffectTimeInterval;
    public float continueTime;
    public VFXConfig vfxConfig;
}

public enum BuffTarget
{
    Self,
    Enemy,
    Area
}

public enum BuffEffect
{
    Damage,
    Heal,
    ChangeProp
}

public enum BuffTakeEffectWay
{
    Once,
    BaseTime
}
