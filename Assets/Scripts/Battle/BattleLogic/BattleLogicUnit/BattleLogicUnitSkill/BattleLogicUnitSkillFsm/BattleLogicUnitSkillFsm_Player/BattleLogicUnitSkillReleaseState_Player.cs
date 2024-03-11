using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitSkillReleaseState_Player : BattleLogicUnitSkillReleaseState
{
    public BattleLogicUnitSkillReleaseState_Player(BattleLogicUnitSkillState state, BattleLogicUnitSkillFsmManager fsmManager) : base(state, fsmManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        EventManager.TriggerEvent(CommonEvent.ON_PLAYER_SKILL_CHANGE_STATE, fsmManager.srcSkill.data.data.id, fsmManager.CurState);
        EventManager.TriggerEvent(CommonEvent.ON_SKILL_USING_OR_CD, fsmManager.srcSkill.data.data.id, false, false);
    }

    public override void Update()
    {
        var curState = fsmManager.srcSkill.GetLogicUnit().GetCurState();
        if (fsmManager.releasing && curState == BattleLogicUnitState.Fight)
        {
            if (fsmManager.releasingTime == 0)
            {
                EventManager.TriggerEvent(CommonEvent.ON_UNIT_RELEASE_SKILL, fsmManager.srcSkill.skillMgr.logicUnitId, fsmManager.srcSkill);
                EventManager.TriggerEvent(CommonEvent.ON_SKILL_USING_OR_CD, fsmManager.srcSkill.data.data.id, true, false);
            }
            fsmManager.releasingTime += BattleManager.Instance._deltaTime;
            var pair = fsmManager.GetSkillTimePair(fsmManager.releasingTime, out bool isLastPair);
            if (pair != null)
            {
                if (!pair.released)
                {
                    pair.released = true;
                    if (pair.id == 1)
                    {
                        SetPairPos(BattleDataRunTimeHelper.GetSkillReleaseTargets(fsmManager.srcSkill.GetLogicUnit().id, pair));
                    }
                    fsmManager.DoDamage(pair);
                    if (isLastPair)
                    {
                        fsmManager.SwitchState(BattleLogicUnitSkillState.After);
                    }
                }
                else
                {
                    SyncPairPos();
                }
            }
        }
    }
    private void SetPairPos(List<int> targets)
    {
        Vector2 pos = Vector2.zero;
        for (int i = 0; i < targets.Count; ++i)
        {
            pos += (Vector2)BattleDataRunTimeHelper.GetLogicUnit(targets[i]).propertyCtrl.curProperties.POS.value;
        }
        pos /= targets.Count;
        for (int i = 0; i < fsmManager.srcSkill.data.skillReleasePairs.Count; ++i)
        {
            fsmManager.srcSkill.data.skillReleasePairs[i].pos = pos;
        }
    }

    private void SyncPairPos()
    {

    }

}
