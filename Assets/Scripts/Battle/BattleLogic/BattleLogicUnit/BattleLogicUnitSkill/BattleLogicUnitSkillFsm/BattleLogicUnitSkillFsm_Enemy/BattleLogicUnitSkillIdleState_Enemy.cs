using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitSkillIdleState_Enemy : BattleLogicUnitSkillIdleState
{
    public BattleLogicUnitSkillIdleState_Enemy(BattleLogicUnitSkillState state, BattleLogicUnitSkillFsmManager fsmManager) : base(state, fsmManager)
    {

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
