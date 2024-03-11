using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Battle Config", menuName = "DataCreate/Buff/New Buff")]
public class BuffData_SO : ScriptableObject
{
    public int id;
    [Header("buff����Ŀ��")] public BuffTarget target;
    public int targetNum;
    [Header("buff��Χ")]public float range;
    [Header("������ֵ")] public float buffValue;
    [Header("���㷽ʽ")] public ValueCalWay calWay;
    [Header("buffЧ��")] public BuffEffect effect;
    [Header("�ı�����ԣ�����effectΪChangePropʱ��Ч")] public LogicUnitProp prop;
    [Header("��Ч��ʽ")] public BuffTakeEffectWay takeEffectWay;
    [Header("��Ч���������takeEffectWayΪBaseTimeʱ��Ч")] public float takeEffectTimeInterval;
    public float continueTime;
    public VFXConfig vfxConfig;
}

public enum BuffTarget
{
    Self,
    Enemy,
    Area
}

public enum BuffEffect
{
    Damage,
    Heal,
    ChangeProp
}

public enum BuffTakeEffectWay
{
    Once,
    BaseTime
}
