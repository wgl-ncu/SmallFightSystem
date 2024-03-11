using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitMoveState_Enemy : BattleLogicUnitMoveState
{
    public BattleLogicUnitMoveState_Enemy(BattleLogicUnitState state, BattleLogicUnitFsmManager fsmManager) : base(state, fsmManager)
    {

    }

    public override void Update()
    {
        base.Update();
        if (fsmManager.enableFight)
        {
            var enemy = BattleDataRunTimeHelper.GetNearestEnemy(fsmManager.srcUnit.id);
            if (enemy != null)
            {
                if (!enemy.isAlive)
                {
                    fsmManager.SwitchState(BattleLogicUnitState.Idle);
                    return;
                }
                var dis = BattleDataRunTimeHelper.GetUnitsDis(fsmManager.srcUnit.id, enemy.id, out int relPos, out var xDis);
                if (dis >= 0)
                {
                    var hasAnySkillCanRelease = fsmManager.HasAnySkillCanRelease(dis);
                    var hasAnySkillReleasing = fsmManager.HasAnySkillReleasing();
                    if ((hasAnySkillCanRelease || hasAnySkillReleasing) && fsmManager.isOnGround)
                    {
                        fsmManager.SwitchState(BattleLogicUnitState.Fight);
                    }
                    else
                    {
                        if (!fsmManager.isOnGround || ((relPos & (int)UnitRelativePos.UP) > 0 && xDis < 1f))
                        {
                            fsmManager.srcUnit.Jump();
                        }
                        else if ((relPos & (int)UnitRelativePos.Bottom) > 0)
                        {
                            if (BattleDataRunTimeHelper.TryGetStandGround(fsmManager.srcUnit.propertyCtrl.curProperties.POS.value, out var g))
                            {
                                if (g.startCanJump)
                                {
                                    fsmManager.srcUnit.direction = -1;
                                    fsmManager.srcUnit.towards = 1;
                                }
                                else if (g.endCanJump)
                                {
                                    fsmManager.srcUnit.direction = 1;
                                    fsmManager.srcUnit.towards = -1;
                                }
                                (fsmManager.srcUnit as BattleLogicUnitEnemy).MoveWithoutRefreshDirection();

                            }
                        }
                        else
                        {
                            fsmManager.srcUnit.Move();
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
