using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFsmFightState : BaseFsm<BattleState>
{
    public BattleFsmManager fsmManager;
    public BattleFsmFightState(BattleState state, BattleFsmManager fsmManager):base(state, fsmManager)
    {
        this.fsmManager = fsmManager;
    }
    public override void Enter()
    {
        
    }

    public override void Leave()
    {
        
    }

    public override void Update()
    {
        
    }
}
