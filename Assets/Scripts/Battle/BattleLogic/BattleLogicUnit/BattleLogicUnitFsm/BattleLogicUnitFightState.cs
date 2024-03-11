using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitFightState : BaseFsm<BattleLogicUnitState>
{
    protected BattleLogicUnitFsmManager fsmManager;
    public BattleLogicUnitFightState(BattleLogicUnitState state, BattleLogicUnitFsmManager fsmManager) :base(state, fsmManager)
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
        if (!fsmManager.srcUnit.isAlive)
        {
            fsmManager.SwitchState(BattleLogicUnitState.Dead);
            return;
        }
    }
}
