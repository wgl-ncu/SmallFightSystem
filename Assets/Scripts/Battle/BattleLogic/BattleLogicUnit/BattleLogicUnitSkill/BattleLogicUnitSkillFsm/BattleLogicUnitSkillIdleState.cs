using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitSkillIdleState : BaseFsm<BattleLogicUnitSkillState>
{
    protected BattleLogicUnitSkillFsmManager fsmManager;
    public BattleLogicUnitSkillIdleState(BattleLogicUnitSkillState state, BattleLogicUnitSkillFsmManager fsmManager):base(state, fsmManager)
    {
        this.fsmManager = fsmManager;
    }

    public override void Enter()
    {
        fsmManager.srcSkill.data.Reset();
    }

    public override void Leave()
    {
    }

    public override void Update()
    {

    }
}
