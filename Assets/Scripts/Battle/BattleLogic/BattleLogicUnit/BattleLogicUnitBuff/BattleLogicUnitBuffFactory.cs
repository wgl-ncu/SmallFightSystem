using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleLogicUnitBuffFactory
{
    public static BattleLogicUnitBuffBase CreateBuff(BattleLogicUnitBuffManager mgr, BuffData_SO buffData)
    {
        switch (buffData.effect)
        {
            case BuffEffect.Damage:
                return new BattleLogicUnitBuff_Damage(mgr, buffData);
            case BuffEffect.Heal:
                return new BattleLogicUnitBuff_Heal(mgr, buffData);
            default:
                Logger.Error("mû�ж�Ӧ��ӦЧ����buff���ͣ�effect��" + buffData.effect, layer: LogLayer.Battle);
                return null;
        }
    }
}
