using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitFightState_Player : BattleLogicUnitFightState
{
    public BattleLogicUnitFightState_Player(BattleLogicUnitState state, BattleLogicUnitFsmManager fsmManager) : base(state,
        fsmManager)
    {
        
    }

    public override void Update()
    {
        base.Update();
        if (!fsmManager.HasAnySkillReleasing())
        {
            fsmManager.SwitchState(BattleLogicUnitState.Move);
        }
    }
}
