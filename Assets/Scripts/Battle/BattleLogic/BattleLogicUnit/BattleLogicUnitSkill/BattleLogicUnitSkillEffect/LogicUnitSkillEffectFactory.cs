using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogicUnitSkillEffectFactory
{
    public static LogicUnitSkillEffectBase GetLogicUnitSkillEffect(SkillReleaseEffect effectType)
    {
        switch (effectType)
        {
            case SkillReleaseEffect.Damage:
                return new SkillEffect_Damage();
            case SkillReleaseEffect.Heal:
                return new SkillEffect_Heal();
            default:
                Logger.Error("û����Ӧ���͵ļ���Ч����effectType: " + effectType);
                return null;

        }
    }
}
