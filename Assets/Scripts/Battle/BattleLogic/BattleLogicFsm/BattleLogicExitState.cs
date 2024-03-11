using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicExitState : BaseFsm<BattleLogicState>
{
    BattleLogicFsmManager fsmManager;
    public BattleLogicExitState(BattleLogicState state, BattleLogicFsmManager fsmManager):base(state, fsmManager)
    {
        this.fsmManager = fsmManager;
    }

    public override void Enter()
    {
        EventManager.TriggerEvent(CommonEvent.ON_ENTER_BATTLE_LOGIC_EXIT_STATE);
    }

    public override void Leave()
    {
        
    }

    public override void Update()
    {
        
    }
}
