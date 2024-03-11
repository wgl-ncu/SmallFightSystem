using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicLoadState : BaseFsm<BattleLogicState>
{
    BattleLogicFsmManager fsmManager;
    public BattleLogicLoadState(BattleLogicState state, BattleLogicFsmManager fsmManager) : base(state, fsmManager)
    {
        this.fsmManager = fsmManager;
    }
    public override void Enter()
    {
        fsmManager.levelLoadFinish = false;
        fsmManager.OnEnterLoadState();//logic layer
        EventManager.TriggerEvent(CommonEvent.ON_ENTER_BATTLE_LOGIC_LOAD_STATE, fsmManager.GetEnemyIds());//view layer
        fsmManager.levelLoadFinish = true;
    }

    public override void Leave()
    {
        fsmManager.OnLeaveLoadState();
        EventManager.TriggerEvent(CommonEvent.ON_LEAVE_BATTLE_LOGIC_LOAD_STATE, fsmManager.GetEnemyIds());//view layer

    }

    public override void Update()
    {
        if (fsmManager.levelLoadFinish)
        {
            fsmManager.SwitchState(BattleLogicState.Enter);
        }
    }
}
