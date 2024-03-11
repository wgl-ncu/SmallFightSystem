using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSkillRuntime
{
    public BattleLogicUnitSkillBase srcSkill;
    public Vector2 skillPos;

    public UnitSkillRuntime(BattleLogicUnitSkillBase skill)
    {
        srcSkill = skill;
    }
}

public class BattleLogicUnitSkillManager
{
    public int logicUnitId;
    public BattleLogicUnitSkillBase normalAtk;
    public List<BattleLogicUnitSkillBase> skills;
    public UnitSkillRuntime curSkill;
    private List<BattleLogicUnitSkillBase> releaseQuene;
    private UnitCamp camp;
    public BattleLogicUnitSkillManager(BattleLogicUnitBase srcUnit)
    {
        logicUnitId = srcUnit.id;
        camp = srcUnit.unitCamp;
        releaseQuene = new List<BattleLogicUnitSkillBase>();
        LoadSkills(srcUnit);
    }

    private void LoadSkills(BattleLogicUnitBase srcUnit)
    {
        normalAtk = new BattleLogicUnitSkillBase(this, srcUnit.propertyCtrl.data.normalAtk, srcUnit.unitCamp);
        skills = new List<BattleLogicUnitSkillBase>();
        var skillIds = srcUnit.propertyCtrl.data.skills;
        for (int i = 0; i < skillIds.Count; ++i)
        {
            skills.Add(new BattleLogicUnitSkillBase(this, skillIds[i], srcUnit.unitCamp));
        }
    }

    public void Update()
    {
        if(curSkill == null)
        {
            if (TryToGetASkillCanRelease(out curSkill))
            {
                
                curSkill.srcSkill.fsmManager.releasing = true;
            }
            else if(camp == UnitCamp.Player)
            {
                ClearReleaseQuene();
            }
        }
        if (null != normalAtk)
        {
            normalAtk.Update();
        }
        for(int i = 0; i < skills.Count; ++i)
        {
            skills[i].Update();
        }

    }

    public bool TryToGetASkillCanRelease(out UnitSkillRuntime skill)
    {
        if (BattleDataRunTimeHelper.GetLogicUnit(logicUnitId).isOnGround)
        {
            var enemy = BattleDataRunTimeHelper.GetNearestEnemy(logicUnitId);
            if (enemy != null)
            {
                var dis = BattleDataRunTimeHelper.GetUnitsDis(logicUnitId, enemy.id, out int relPos, out var xDis);
                var index = releaseQuene.FindIndex((a) => a.data.skillReleasePairs[0].releaseData.range >= dis || a.data.skillReleasePairs[0].releaseData.selectWay == SkillReleaseTargetSelect.Self);
                if (index != -1)
                {
                    skill = new UnitSkillRuntime(releaseQuene[index]);
                    releaseQuene.RemoveAt(index);
                    return true;
                }
            }
        }
        skill = null;
        return false;
    }

    internal void ClearReleaseQuene()
    {
        releaseQuene.Clear();
    }

    public bool HasAnySkillReleasing()
    {
        return curSkill != null;
    }

    public bool HasAnySkillCanRelease(float dis)
    {
        return releaseQuene.FindIndex((a) => a.data.skillReleasePairs[0].releaseData.range >= dis) != -1;
    }

    public bool HasAnySkillWaitForRelease()
    {
        return releaseQuene.Count > 0;
    }

    internal void TryToRelease(BattleLogicUnitSkillBase skill)
    {
        releaseQuene.Add(skill);
    }

    public BattleLogicUnitBase GetLogicUnit()
    {
        return BattleDataRunTimeHelper.GetLogicUnit(logicUnitId);
    }

    public BattleLogicUnitSkillBase GetSkill(int skillId)
    {
        if(skillId == normalAtk.data.data.id)
        {
            return normalAtk;
        }
        return skills.Find(a => a.data.data.id == skillId);
    }
}
