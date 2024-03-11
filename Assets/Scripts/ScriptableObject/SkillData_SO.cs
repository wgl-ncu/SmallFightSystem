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
    [Header("���ܴ���")] public float release;
    public float effectValue;
    [Header("Ч�����㷽ʽ")]public ValueCalWay calWay;
    [Header("���ʩ������")] public float range;
    [Header("������Χ�������ͷź����ͷŵ���Χ���ٷ�Χ�������У�")] public float scope;
    [Header("����Ч������")]public SkillReleaseEffect effect;
    public SkillReleaseTargetSelect selectWay;
    [Header("Ŀ������-1Ϊ��Χ��ȫ��")] public int tarNum;
    [Header("���buff")] public List<int> buffs;
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
