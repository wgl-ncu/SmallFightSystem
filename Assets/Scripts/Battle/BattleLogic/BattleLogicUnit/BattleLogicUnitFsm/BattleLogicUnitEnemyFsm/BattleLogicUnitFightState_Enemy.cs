using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitFightState_Enemy : BattleLogicUnitFightState
{
    public BattleLogicUnitFightState_Enemy(BattleLogicUnitState state, BattleLogicUnitFsmManager fsmManager) : base(state, fsmManager)
    {

    }

    public override void Update()
    {
        base.Update();
        if (fsmManager.enableFight)
        {
            var player = BattleDataRunTimeHelper.GetNearestEnemy(fsmManager.srcUnit.id);
            if (player != null)
            {
                if (!player.isAlive)
                {
                    fsmManager.SwitchState(BattleLogicUnitState.Idle);
                    return;
                }
                var dis = BattleDataRunTimeHelper.GetUnitsDis(fsmManager.srcUnit.id, player.id, out int relPos, out float xDis);
                if (dis >= 0)
                {
                    var hasAnySkillReleasing = fsmManager.HasAnySkillReleasing();
                    var hasAnySkillCanRelease = fsmManager.HasAnySkillCanRelease(dis);
                    var hasAnySkillWaitForRelease = fsmManager.HasAnySkillWaitForRelease();

                    if (!hasAnySkillCanRelease && !hasAnySkillReleasing)
                    {
                        if (hasAnySkillWaitForRelease)
                        {
                            fsmManager.SwitchState(BattleLogicUnitState.Move);
                        }
                        else
                        {
                            fsmManager.SwitchState(BattleLogicUnitState.Idle);
                        }
                    }
                }
            }
        }
        else
        {
            fsmManager.SwitchState(BattleLogicUnitState.Idle);
        }
    }
}
