using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitBuff_Damage : BattleLogicUnitBuffBase
{
    public BattleLogicUnitBuff_Damage(BattleLogicUnitBuffManager mgr, BuffData_SO data_SO) : base(mgr, data_SO)
    {

    }

    public override PropChangeData CreatePropChangeData(int unitid)
    {
        var unit = BattleDataRunTimeHelper.GetLogicUnit(unitid);
        var data = new PropChangeData();
        var effectValue = this.data.buffValue;
        int value = 0;
        switch (this.data.calWay)
        {
            case ValueCalWay.Fixed:
                value = (int)-effectValue;
                break;
            case ValueCalWay.BaseATK:
                value = -(int)(unit.propertyCtrl.curProperties.ATK.value * effectValue);
                break;
            case ValueCalWay.BaseHP:
                value = -(int)(unit.propertyCtrl.curProperties.HP.value * effectValue);
                break;
            default:
                Logger.Error("没有相应的计算方式！calway：" + this.data.calWay, layer: LogLayer.Battle);
                break;
        }
        data.Add(LogicUnitProp.HP, value);
        return data;
    }

    protected override void GetDispatchTarget(out bool dispatchPlayer, out bool dispatchEnemy, out bool dispatchSelf)
    {
        bool isPlayer = buffManager.srcUnit.unitCamp == UnitCamp.Player;
        dispatchPlayer = isPlayer;
        dispatchEnemy = !isPlayer;
        dispatchSelf = true;
    }
}
