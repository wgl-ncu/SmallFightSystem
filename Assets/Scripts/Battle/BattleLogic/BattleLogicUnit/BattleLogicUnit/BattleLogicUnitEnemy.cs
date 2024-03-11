using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitEnemy : BattleLogicUnitBase
{
    public BattleLogicUnitEnemy(CharacterData_SO data, UnitCamp camp) : base(data, camp)
    {

    }

    public override void RefreshDirection()
    {
        var playerPos = BattleDataRunTimeHelper.GetLogicPlayer().propertyCtrl.curProperties.POS.value.x;
        var selfPos = propertyCtrl.curProperties.POS.value.x;
        bool isPlayerInSelfLeft = playerPos < selfPos;
        direction = isPlayerInSelfLeft ? -1 : 1;
        towards = -direction;
    }

    public void MoveWithoutRefreshDirection()
    {
        float moveDis = propertyCtrl.curProperties.speed.value * direction * 0.1f;
        Vector3 movePosV3 = new Vector3(moveDis, 0, 0);
        var propChange = new PropChangeData();
        propChange.Add(LogicUnitProp.POS, movePosV3);
        DealPropChange(propChange);
        CheckIsOnGround();
    }
}
