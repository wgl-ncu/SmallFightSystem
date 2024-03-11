using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleViewUnitDeadState : BaseFsm<BattleViewUnitState>
{
    BattleViewUnitFsmManager fsmManager;
    public BattleViewUnitDeadState(BattleViewUnitState state, BattleViewUnitFsmManager fsmManager):base(state, fsmManager)
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
