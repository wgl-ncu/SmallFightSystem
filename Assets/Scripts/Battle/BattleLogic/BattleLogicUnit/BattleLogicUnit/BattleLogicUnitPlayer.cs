using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicUnitPlayer : BattleLogicUnitBase
{
    //public KeyCode curKeyCode = KeyCode.None;
    public BattleLogicUnitPlayer(CharacterData_SO data, UnitCamp camp) : base(data, camp)
    {

    }

    internal void OnGetInputKey(KeyCode key)
    {
        fsmManager.OnGetInputKey(key);
        if ((fsmManager.CurState == BattleLogicUnitState.Idle || fsmManager.CurState == BattleLogicUnitState.Move))
        {
            direction = key == KeyCode.D ? 1 : -1;
            towards = direction;
        }
    }


    public override void Move()
    {
        int direction = this.direction;
        float moveDis = propertyCtrl.curProperties.speed.value * direction * 0.1f;
        Vector3 movePosV3 = new Vector3(moveDis, 0, 0);
        var propChange = new PropChangeData();
        propChange.Add(LogicUnitProp.POS, movePosV3);
        DealPropChange(propChange);
        CheckIsOnGround();
    }

    internal void OnInputNoneKey()
    {
        fsmManager.OnGetInputKey(KeyCode.None);
    }

    internal void OnPressSkillBtn(int skillid)
    {
        var skill = skillManager.GetSkill(skillid);
        skill.fsmManager.TryToRelease();
    }

    internal void OnPressKey(KeyCode key)
    {
        fsmManager.OnPressKey(key);
    }
}
