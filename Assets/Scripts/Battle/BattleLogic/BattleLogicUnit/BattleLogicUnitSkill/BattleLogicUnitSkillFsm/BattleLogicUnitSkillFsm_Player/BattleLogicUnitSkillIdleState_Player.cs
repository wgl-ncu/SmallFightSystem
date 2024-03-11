using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitSkillIdleState_Player : BattleLogicUnitSkillIdleState
{
    public BattleLogicUnitSkillIdleState_Player(BattleLogicUnitSkillState state, BattleLogicUnitSkillFsmManager fsmManager) : base(state, fsmManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        EventManager.TriggerEvent(CommonEvent.ON_PLAYER_SKILL_CHANGE_STATE, fsmManager.srcSkill.data.data.id, fsmManager.CurState);
        EventManager.TriggerEvent(CommonEvent.ON_SKILL_USING_OR_CD, fsmManager.srcSkill.data.data.id, false, true);
    }

    public override void Update()
    {
        fsmManager.cd -= BattleManager.Instance._deltaTime;

        if (fsmManager.cd <= 0)
        {
            fsmManager.SwitchState(BattleLogicUnitSkillState.Release);
        }
    }
}
