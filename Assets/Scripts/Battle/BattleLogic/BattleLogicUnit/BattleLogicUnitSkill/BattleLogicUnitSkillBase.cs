using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillReleasePair
{
    public int id;
    public float release;
    public float damage;
    public bool released;
    public SkillReleaseTime releaseData;
    public LogicUnitSkillEffectBase effect;
    public Vector3 pos;

    public SkillReleasePair(SkillReleaseTime skillReleaseTime, int id)
    {
        release = skillReleaseTime.release;
        damage = skillReleaseTime.effectValue;
        released = false;
        releaseData = skillReleaseTime;
        effect = LogicUnitSkillEffectFactory.GetLogicUnitSkillEffect(skillReleaseTime.effect);
        effect.Init(skillReleaseTime);
        this.id = id;
        pos = Vector3.zero;
    }
}
public class SkillData
{
    public SkillData_SO data;
    public List<SkillReleasePair> skillReleasePairs;
    public SkillData(SkillData_SO data_SO)
    {
        data = data_SO;
        skillReleasePairs = new List<SkillReleasePair>();
        for(int i = 0; i < data_SO.releasePairs.Count; ++i)
        {
            skillReleasePairs.Add(new SkillReleasePair(data_SO.releasePairs[i], i + 1));
        }
        skillReleasePairs.Sort((a, b)=> {
            return a.release > b.release ? 1 : a.release == b.release ? 0 : -1;
        });
    }

    public void Reset()
    {
        for(int i = 0; i < skillReleasePairs.Count; ++i)
        {
            skillReleasePairs[i].released = false;
        }
    }
}
public class BattleLogicUnitSkillBase
{
    public BattleLogicUnitSkillManager skillMgr;
    public SkillData data;
    public BattleLogicUnitSkillFsmManager fsmManager;

    public BattleLogicUnitSkillBase(BattleLogicUnitSkillManager skillMgr, int skillId, UnitCamp camp)
    {
        this.skillMgr = skillMgr;
        data = new SkillData(BattleDataHelper.GetSkillData(skillId));
        fsmManager = new BattleLogicUnitSkillFsmManager(this, camp);
    }

    public void Update()
    {
        fsmManager.Update();
    }

    public BattleLogicUnitBase GetLogicUnit()
    {
        return skillMgr.GetLogicUnit();
    }

    internal void TryToRelease()
    {
        skillMgr.TryToRelease(this);
    }

    public void ClearReleaseQuene()
    {
        skillMgr.ClearReleaseQuene();
    }

    internal SkillReleasePair GetSkillTimePair(float time, out bool isLastPair)
    {
        var pairs = data.skillReleasePairs;
        for (int i = 0; i < pairs.Count - 1;  ++i)
        {
            if(time >= pairs[i].release && time < pairs[i + 1].release)
            {
                isLastPair = false;
                return pairs[i];
            }
        }
        if(time >= pairs[pairs.Count - 1].release && time < data.data.time)
        {
            isLastPair = true;
            return pairs[pairs.Count - 1];
        }
        isLastPair = false;
        return null;
    }
}
