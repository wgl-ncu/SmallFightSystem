using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LogicUnitSkillEffectBase
{
    public SkillReleaseEffect effectType;
    public ValueCalWay calWay;
    public float effectValue;
    public virtual void Init(SkillReleaseTime data)
    {
        effectType = data.effect;
        effectValue = data.effectValue;
        calWay = data.calWay;
    }
    public abstract PropChangeData CreatePropChangeData(int unitId);
}
