using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleLogicUnitSkillState
{
    Idle,
    Release,
    After
}
public class BattleLogicUnitSkillFsmManager : BaseFsmManager<BattleLogicUnitSkillState>
{
    public BattleLogicUnitSkillBase srcSkill;
    public bool releasing;
    public float releasingTime;
    public float cd;
    public UnitCamp camp;
    public BattleLogicUnitSkillFsmManager(BattleLogicUnitSkillBase skill, UnitCamp camp)
    {
        srcSkill = skill;
        this.camp = camp;
        cd = 0;
        fsmDict = new Dictionary<BattleLogicUnitSkillState, BaseFsm<BattleLogicUnitSkillState>>();
        if(camp == UnitCamp.Player)
        {
            AddPlayerState();
        }
        else
        {
            AddEnemyState();
        }
        SwitchState(BattleLogicUnitSkillState.Idle);
    }

    private void AddPlayerState()
    {
        fsmDict.Add(BattleLogicUnitSkillState.Idle, new BattleLogicUnitSkillIdleState_Player(BattleLogicUnitSkillState.Idle, this));
        fsmDict.Add(BattleLogicUnitSkillState.Release, new BattleLogicUnitSkillReleaseState_Player(BattleLogicUnitSkillState.Release, this));
        fsmDict.Add(BattleLogicUnitSkillState.After, new BattleLogicUnitSkillAfterState_Player(BattleLogicUnitSkillState.After, this));

    }

    private void AddEnemyState()
    {
        fsmDict.Add(BattleLogicUnitSkillState.Idle, new BattleLogicUnitSkillIdleState_Enemy(BattleLogicUnitSkillState.Idle, this));
        fsmDict.Add(BattleLogicUnitSkillState.Release, new BattleLogicUnitSkillReleaseState_Enemy(BattleLogicUnitSkillState.Release, this));
        fsmDict.Add(BattleLogicUnitSkillState.After, new BattleLogicUnitSkillAfterState_Enemy(BattleLogicUnitSkillState.After, this));

    }

    internal void TryToRelease()
    {
        if (camp == UnitCamp.Enemy)
        {
            srcSkill.TryToRelease();
        }
        else
        {
            if (!srcSkill.skillMgr.HasAnySkillReleasing())
            {
                srcSkill.ClearReleaseQuene();
                srcSkill.TryToRelease();
                releasing = false;
                releasingTime = 0;
            }
        }
    }

    public SkillReleasePair GetSkillTimePair(float time, out bool isLastPair)
    {
        return srcSkill.GetSkillTimePair(time, out isLastPair);
    }

    public void DoDamage(SkillReleasePair pair)
    {
        var unitId = srcSkill.skillMgr.logicUnitId;
        var unit = BattleDataRunTimeHelper.GetLogicUnit(unitId);
        var buffSelf = new List<BuffData_SO>();
        var buffTargets = new List<BuffData_SO>();
        for (int j = 0; j < pair.releaseData.buffs.Count; ++j)
        {
            var buffId = pair.releaseData.buffs[j];
            var buff = BattleDataHelper.GetBuffData(buffId);
            if (buff.target == BuffTarget.Enemy)
            {
                buffTargets.Add(buff);
            }
            else
            {
                buffSelf.Add(buff);
            }
        }
        for(int i = 0; i < buffSelf.Count; ++i)
        {
            unit.AddBuff(buffSelf[i], true);
        }
        PropChangeData data = pair.effect.CreatePropChangeData(unitId);
        var targets = BattleDataRunTimeHelper.GetSkillReleaseScopeTargets(unitId, pair);
        for(int i = 0; i < targets.Count; ++i)
        {
            var target = BattleDataRunTimeHelper.GetLogicUnit(targets[i]);
            target.DealPropChange(data);
            for(int j = 0; j < buffTargets.Count; ++j)
            {
                target.AddBuff(buffTargets[j], false);
            }
        }
    }
}
