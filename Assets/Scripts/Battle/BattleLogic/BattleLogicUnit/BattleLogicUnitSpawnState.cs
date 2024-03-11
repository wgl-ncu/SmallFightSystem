using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitSpawnState : BaseFsm<BattleLogicUnitState>
{
    protected BattleLogicUnitFsmManager fsmManager;
    private float passTime;
    public BattleLogicUnitSpawnState(BattleLogicUnitState state, BattleLogicUnitFsmManager fsmManager) : base(state, fsmManager)
    {
        this.fsmManager = fsmManager;
        passTime = 0;
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
        passTime += BattleManager.Instance._deltaTime;
        if (!fsmManager.srcUnit.isAlive)
        {
            fsmManager.SwitchState(BattleLogicUnitState.Dead);
            return;
        }
        if(passTime >= fsmManager.srcUnit.propertyCtrl.data.spawnTime)
        {
            fsmManager.SwitchState(BattleLogicUnitState.Idle);
        }
    }
}
