using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect_Damage : LogicUnitSkillEffectBase
{
    public override PropChangeData CreatePropChangeData(int unitId)
    {
        var unit = BattleDataRunTimeHelper.GetLogicUnit(unitId);
        PropChangeData data = new PropChangeData();
        int value = 0;
        switch (calWay)
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
                Logger.Error("没有相应的计算方式！calway：" + calWay, layer: LogLayer.Battle);
                break;
        }
        data.Add(LogicUnitProp.HP, value);
        return data;
    }
}
