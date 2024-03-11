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
        //ս��״̬�У�����е����ҵ����ڹ�����Χ������빥��״̬�����е��˵������ڹ�����Χ�����ƶ�
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
