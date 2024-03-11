using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitSkillAfterState : BaseFsm<BattleLogicUnitSkillState>
{
    protected BattleLogicUnitSkillFsmManager fsmManager;
    public BattleLogicUnitSkillAfterState(BattleLogicUnitSkillState state, BattleLogicUnitSkillFsmManager fsmManager):base(state, fsmManager)
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
