using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitBuffManager
{
    public BattleLogicUnitBase srcUnit;

    public List<BattleLogicUnitBuffBase> dispatchBuffs;//自己产生的buff
    public List<BattleLogicUnitBuffBase> acceptBuffs;//挂在自己身上的buff
    public BattleLogicUnitBuffManager(BattleLogicUnitBase unit)
    {
        srcUnit = unit;
        dispatchBuffs = new List<BattleLogicUnitBuffBase>();
        acceptBuffs = new List<BattleLogicUnitBuffBase>();
    }

    public void Update()
    {
        for (int i = 0; i < dispatchBuffs.Count; ++i)
        {
            dispatchBuffs[i].Update();
        }
        for (int i = 0; i < acceptBuffs.Count; ++i)
        {
            acceptBuffs[i].Update();
        }
    }

    public void DispatchBuff(BattleLogicUnitBuffBase buff)
    {
        dispatchBuffs.Add(buff);
        buff.Start();
    }

    public void AcceptBuff(BattleLogicUnitBuffBase buff)
    {
        acceptBuffs.Add(buff);
        buff.Start();
    }

    public void UnloadBuff(int buffRunTimeId)
    {
        var buffDispatch = dispatchBuffs.Find(a => a.runTimeId == buffRunTimeId);
        if (buffDispatch != null)
        {
            dispatchBuffs.Remove(buffDispatch);
        }
        var buffAccept = acceptBuffs.Find(a => a.runTimeId == buffRunTimeId);
        if(buffAccept != null)
        {
            acceptBuffs.Remove(buffAccept);
        }
    }

    internal void AddBuff(BuffData_SO buffData, bool isSelf)
    {
        var buff = BattleLogicUnitBuffFactory.CreateBuff(this, buffData);
        if(buff.data.target == BuffTarget.Area)
        {
            buff.SetPos(srcUnit.propertyCtrl.curProperties.POS.value);
        }
        if (isSelf)
        {
            DispatchBuff(buff);
        }
        else
        {
            AcceptBuff(buff);
        }
    }
}
