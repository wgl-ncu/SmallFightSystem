using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleViewUnitFightState : BaseFsm<BattleViewUnitState>
{
    BattleViewUnitFsmManager fsmManager;
    public BattleViewUnitFightState(BattleViewUnitState state, BattleViewUnitFsmManager fsmManager):base(state, fsmManager)
    {
        this.fsmManager = fsmManager;
    }
    public override void Enter()
    {
        //fsmManager.viewUnit.animCtrl.SwitchState(_state);
    }

    public override void Leave()
    {
        
    }

    public override void Update()
    {
        
    }
}
