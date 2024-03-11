using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitIdleState_Player : BattleLogicUnitIdleState
{
    public BattleLogicUnitIdleState_Player(BattleLogicUnitState state, BattleLogicUnitFsmManager fsmManager):base(state, fsmManager)
    {

    }

    public override void Update()
    {
        base.Update();
        if (!fsmManager.isOnGround || fsmManager.curPressKeyCode == KeyCode.W)
        {
            fsmManager.srcUnit.Jump();
            fsmManager.curPressKeyCode = KeyCode.None;
        }
        if (fsmManager.curGetKeyCode != KeyCode.None)
        {
            fsmManager.SwitchState(BattleLogicUnitState.Move);
            return;
        }
        if (fsmManager.srcUnit.skillManager.HasAnySkillReleasing())
        {
            fsmManager.SwitchState(BattleLogicUnitState.Fight);
            return;
        }
    }
}
