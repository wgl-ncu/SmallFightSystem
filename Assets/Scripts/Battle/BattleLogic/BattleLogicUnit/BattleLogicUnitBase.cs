using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitCamp
{
    Player,
    Enemy
}
public class BattleLogicUnitBase
{
    public int id;
    public BattleLogicUnitFsmManager fsmManager;
    public BattleLogicUnitSkillManager skillManager;
    public BattleLogicUnitPropertyCtrl propertyCtrl;
    public BattleLogicUnitBuffManager buffManager;
    public UnitCamp unitCamp;
    public bool isOnGround;
    public bool started;

    public bool isAlive => propertyCtrl.curProperties.HP.value > 0;

    public int direction = 1;
    public int towards = 1;

    private int jumpPower;

    public BattleLogicUnitBase(CharacterData_SO data, UnitCamp camp)
    {
        id = BattleLogicUnitManager.ApplyUnitId();
        isOnGround = true;
        started = false;
        unitCamp = camp;
        buffManager = new BattleLogicUnitBuffManager(this);
        fsmManager = new BattleLogicUnitFsmManager(this);
        propertyCtrl = new BattleLogicUnitPropertyCtrl(this, data);
        skillManager = new BattleLogicUnitSkillManager(this);
    }

    public void Uninit()
    {

    }

    public void Start()
    {
        fsmManager.SwitchState(BattleLogicUnitState.Spawn);
        started = true;
    }

    public string GetPrefabPath()
    {
        return propertyCtrl.data.assetPath;
    }

    public void Update()
    {
        if (!started)
        {
            Start();
        }
        else
        {
            fsmManager.Update();
            skillManager.Update();
            buffManager.Update();
        }
    }

    public virtual void Move()
    {
        RefreshDirection();
        float moveDis =  propertyCtrl.curProperties.speed.value * direction * 0.1f;
        Vector3 movePosV3 = new Vector3(moveDis, 0, 0);
        var propChange = new PropChangeData();
        propChange.Add(LogicUnitProp.POS, movePosV3);
        DealPropChange(propChange);
        CheckIsOnGround();
    }

    protected void CheckIsOnGround()
    {
        isOnGround = BattleDataRunTimeHelper.IsOnGround(propertyCtrl.curProperties.POS.value);
    }

    public virtual void Jump()
    {
        var curPos = propertyCtrl.curProperties.POS.value;
        if (isOnGround)
        {
            jumpPower = 30;
            isOnGround = false;
            _Jump(curPos);
        }
        else
        {
            if (!BattleDataRunTimeHelper.IsOnGround(curPos))
            {
                _Jump(curPos);
            }
            else
            {
                isOnGround = true;
                jumpPower = 0;
            }
        }
    }

    private void _Jump(Vector3 curPos)
    {
        float jumpHeight = jumpPower * 0.01f;
        Vector3 jumpV3 = new Vector3(0, jumpHeight, 0);
        jumpV3 = BattleDataRunTimeHelper.AdjustJumpV3(curPos, jumpV3);
        var propChange = new PropChangeData();
        propChange.Add(LogicUnitProp.POS, jumpV3);
        DealPropChange(propChange);
        jumpPower -= 1;
    }

    public virtual void RefreshDirection()
    {
        direction = unitCamp == UnitCamp.Player ? 1 : -1;
        towards = direction;
    }

    public void DealPropChange(PropChangeData data)
    {
        for(int i = 0; i < data.intProps.Count; ++i)
        {
            propertyCtrl.DealPropChange(data.intProps[i].Key, data.intProps[i].Value);
        }

        for (int i = 0; i < data.floatProps.Count; ++i)
        {
            propertyCtrl.DealPropChange(data.floatProps[i].Key, data.floatProps[i].Value);
        }

        for (int i = 0; i < data.v3Props.Count; ++i)
        {
            propertyCtrl.DealPropChange(data.v3Props[i].Key, data.v3Props[i].Value);
        }
    }

    public void AddBuff(BuffData_SO buffData, bool isSelf)
    {
        buffManager.AddBuff(buffData, isSelf);
    }

    public BattleLogicUnitState GetCurState()
    {
        return fsmManager.CurState;
    }
}
