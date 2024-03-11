using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitIdleState_Enemy : BattleLogicUnitIdleState
{
    public BattleLogicUnitIdleState_Enemy(BattleLogicUnitState state, BattleLogicUnitFsmManager fsmManager) : base(state, fsmManager)
    {

    }

    public override void Update()
    {
        base.Update();
        //战斗状态中，如果有敌人且敌人在攻击范围内则进入攻击状态，若有敌人但并不在攻击范围内则移动
        if (fsmManager.enableFight)
        {
            var player = BattleDataRunTimeHelper.GetNearestEnemy(fsmManager.srcUnit.id);
            if (player != null && player.isAlive)
            {
                var dis = BattleDataRunTimeHelper.GetUnitsDis(fsmManager.srcUnit.id, player.id, out int relPos, out var xDis);
                if (dis >= 0)
                {
                    var hasAnySkillCanRelease = fsmManager.HasAnySkillCanRelease(dis);
                    var hasAnySkillWaitForRelease = fsmManager.HasAnySkillWaitForRelease();
                    if (hasAnySkillCanRelease)
                    {
                        fsmManager.SwitchState(BattleLogicUnitState.Fight);
                    }
                    else if (hasAnySkillWaitForRelease)
                    {
                        fsmManager.SwitchState(BattleLogicUnitState.Move);
                    }
                }
            }
        }
    }
}
