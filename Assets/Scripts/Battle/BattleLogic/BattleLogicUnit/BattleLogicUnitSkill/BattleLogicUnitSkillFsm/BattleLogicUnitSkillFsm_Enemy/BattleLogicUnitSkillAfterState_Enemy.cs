using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitSkillAfterState_Enemy : BattleLogicUnitSkillAfterState
{
    public BattleLogicUnitSkillAfterState_Enemy(BattleLogicUnitSkillState state, BattleLogicUnitSkillFsmManager fsmManager) : base(state, fsmManager)
    {

    }

    public override void Update()
    {
        fsmManager.releasingTime += BattleManager.Instance._deltaTime;
        if(fsmManager.releasingTime >= fsmManager.srcSkill.data.data.time)
        {
            fsmManager.cd = fsmManager.srcSkill.data.data.cd;
            fsmManager.srcSkill.skillMgr.curSkill = null;
            fsmManager.SwitchState(BattleLogicUnitSkillState.Idle);
        }
    }
}
