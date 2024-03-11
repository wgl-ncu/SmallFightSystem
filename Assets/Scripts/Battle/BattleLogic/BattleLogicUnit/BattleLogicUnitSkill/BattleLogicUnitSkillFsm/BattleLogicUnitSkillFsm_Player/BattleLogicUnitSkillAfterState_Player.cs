using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitSkillAfterState_Player : BattleLogicUnitSkillAfterState
{
    public BattleLogicUnitSkillAfterState_Player(BattleLogicUnitSkillState state, BattleLogicUnitSkillFsmManager fsmManager):base(state, fsmManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        EventManager.TriggerEvent(CommonEvent.ON_PLAYER_SKILL_CHANGE_STATE, fsmManager.srcSkill.data.data.id, fsmManager.CurState);
    }

    public override void Update()
    {
        fsmManager.releasingTime += BattleManager.Instance._deltaTime;
        if (fsmManager.releasingTime >= fsmManager.srcSkill.data.data.time)
        {
            fsmManager.cd = fsmManager.srcSkill.data.data.cd;
            fsmManager.srcSkill.skillMgr.curSkill = null;
            fsmManager.SwitchState(BattleLogicUnitSkillState.Idle);
        }
    }
}
