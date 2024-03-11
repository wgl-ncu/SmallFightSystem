using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicAttackingState : BaseFsm<BattleLogicState>
{
    BattleLogicFsmManager fsmManager;
    public BattleLogicAttackingState(BattleLogicState state, BattleLogicFsmManager fsmManager):base(state, fsmManager)
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
        if (!fsmManager.battleLogicManager.HasAnyEnemyAlive())
        {
            fsmManager.battleLogicManager.OnLevelWin();
            fsmManager.SwitchState(BattleLogicState.Load);
        }
    }
}
