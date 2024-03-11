using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "new Skill Data", menuName = "DataCreate/SkillData/Data")]
public class SkillData_SO : ScriptableObject
{
    public int id;
    public string skillName;
    public float cd;
    public List<SkillReleaseTime> releasePairs;
    public float time;
    public string animName;
    public List<VFXConfig> startVFXs;
}

[Serializable]
public class SkillReleaseTime
{
    [Header("技能触发")] public float release;
    public float effectValue;
    [Header("效果计算方式")]public ValueCalWay calWay;
    [Header("最大施法距离")] public float range;
    [Header("触发范围（技能释放后在释放点周围多少范围内能命中）")] public float scope;
    [Header("技能效果类型")]public SkillReleaseEffect effect;
    public SkillReleaseTargetSelect selectWay;
    [Header("目标数，-1为范围内全部")] public int tarNum;
    [Header("添加buff")] public List<int> buffs;
}

[Serializable]
public class VFXConfig
{
    public string path;
    public float offset;
    public SkillVFXLoadWay loadWay;
}

public enum SkillReleaseEffect
{
    Damage,
    Heal
}

public enum SkillReleaseTargetSelect
{
    Nearest,
    Farthest,
    Self
}

public enum SkillVFXLoadWay
{
    Self,
    FirstTarget,
    Center
}

public enum ValueCalWay
{
    Fixed,
    BaseATK,
    BaseHP
}
