using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleViewUnitIdleState : BaseFsm<BattleViewUnitState>
{
    BattleViewUnitFsmManager fsmManager;
    public BattleViewUnitIdleState(BattleViewUnitState state, BattleViewUnitFsmManager fsmManager):base(state, fsmManager)
    {
        this.fsmManager = fsmManager;
    }
    public override void Enter()
    {
        fsmManager.viewUnit.animCtrl.SwitchState(_state);
    }

    public override void Leave()
    {
        
    }

    public override void Update()
    {
        
    }
}
