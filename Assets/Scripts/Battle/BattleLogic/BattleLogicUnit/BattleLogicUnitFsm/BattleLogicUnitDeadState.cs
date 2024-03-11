using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitDeadState : BaseFsm<BattleLogicUnitState>
{
    BattleLogicUnitFsmManager fsmManager;
    public BattleLogicUnitDeadState(BattleLogicUnitState state, BattleLogicUnitFsmManager fsmManager) :base(state, fsmManager)
    {
        this.fsmManager = fsmManager;
    }
    public override void Enter()
    {
        EventManager.TriggerEvent(CommonEvent.ON_BATTLE_LOGIC_UNIT_CHANGE_STATE, fsmManager.srcUnit.id, _state);
    }

    public override void Leave()
    {
        
    }

    public override void Update()
    {
        
    }
}
