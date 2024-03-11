using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleLogicUnitBuffBase
{
    public int runTimeId;
    public BattleLogicUnitBuffManager buffManager;
    public BuffData_SO data;
    public float passTime;
    public float intervalTime;
    public Vector3 pos;
    public int unitId => buffManager.srcUnit.id;
    public BattleLogicUnitBuffBase(BattleLogicUnitBuffManager mgr, BuffData_SO data_SO)
    {
        runTimeId = BattleLogicUnitBuffBase.ApplyId();
        buffManager = mgr;
        data = data_SO;
        passTime = 0;
        intervalTime = 0;
    }

    public void SetPos(Vector3 pos)
    {
        this.pos = pos;
    }

    public virtual void Start()
    {
        if(data.takeEffectWay == BuffTakeEffectWay.Once)
        {
            DoEffect();
        }
        EventManager.TriggerEvent(CommonEvent.ON_BUFF_START, this);
    }

    public virtual void Update()
    {
        UpdateTime();
        if (data.takeEffectWay == BuffTakeEffectWay.BaseTime)
        {
            if (intervalTime >= data.takeEffectTimeInterval)
            {
                DoEffect();
                intervalTime = 0;
            }
        }
        if (passTime >= data.continueTime)
        {
            End();
        }
    }

    public virtual void DoEffect()
    {
        var targets = GetTarget();
        var propData = CreatePropChangeData(buffManager.srcUnit.id);
        for (int i = 0; i < targets.Count; ++i)
        {
            var unit = BattleDataRunTimeHelper.GetLogicUnit(targets[i]);
            unit.DealPropChange(propData);
        }
    }

    public virtual void End()
    {
        buffManager.UnloadBuff(runTimeId);
    }

    protected void UpdateTime()//在两个更新中执行一次
    {
        passTime += BattleManager.Instance._deltaTime;
        if(data.takeEffectWay == BuffTakeEffectWay.BaseTime)
        {
            intervalTime += BattleManager.Instance._deltaTime;
        }
    }

    public List<int> GetTarget()
    {
        List<int> res = new List<int>();
        switch (data.target)
        {
            case BuffTarget.Self:
            case BuffTarget.Enemy:
                res.Add(buffManager.srcUnit.id);
                break;
            case BuffTarget.Area:
                GetDispatchTarget(out bool dispatchPlayer, out bool dispatchEnemy, out bool dispatchSelf);
                res = BattleDataRunTimeHelper.GetUnitsInArea(pos, data.range, data.targetNum, dispatchPlayer, dispatchEnemy);
                if(dispatchSelf && res.Contains(buffManager.srcUnit.id))
                {
                    res.Remove(buffManager.srcUnit.id);
                }
                break;
        }
        return res;
    }

    protected abstract void GetDispatchTarget(out bool dispatchPlayer, out bool dispatchEnemy, out bool dispatchSelf);

    public abstract PropChangeData CreatePropChangeData(int unitId);

    #region tool

    private static int runTimeIdGlobal = 0;
    public static int ApplyId()
    {
        return ++runTimeIdGlobal;
    }

    #endregion
}
