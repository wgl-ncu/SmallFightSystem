using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleLogicUnitState
{
    Spawn,
    Idle,
    Move,
    Fight,
    Dead
}
public class BattleLogicUnitFsmManager : BaseFsmManager<BattleLogicUnitState>
{
    public BattleLogicUnitBase srcUnit;
    public bool enableFight;
    public KeyCode curGetKeyCode;
    public KeyCode curPressKeyCode;
    public bool isOnGround => srcUnit.isOnGround;
    public BattleLogicUnitFsmManager(BattleLogicUnitBase unit)
    {
        srcUnit = unit;
        enableFight = false;
        fsmDict = new Dictionary<BattleLogicUnitState, BaseFsm<BattleLogicUnitState>>();
        curGetKeyCode = KeyCode.None;
        curPressKeyCode = KeyCode.None;
        if(unit.unitCamp == UnitCamp.Player)
        {
            AddPlayerState();
        }
        else
        {
            AddEnemyState();
        }
    }

    private void AddPlayerState()
    {
        fsmDict.Add(BattleLogicUnitState.Spawn, new BattleLogicUnitSpawnState(BattleLogicUnitState.Spawn, this));
        fsmDict.Add(BattleLogicUnitState.Idle, new BattleLogicUnitIdleState_Player(BattleLogicUnitState.Idle, this));
        fsmDict.Add(BattleLogicUnitState.Move, new BattleLogicUnitMoveState_Player(BattleLogicUnitState.Move, this));
        fsmDict.Add(BattleLogicUnitState.Fight, new BattleLogicUnitFightState_Player(BattleLogicUnitState.Fight, this));
        fsmDict.Add(BattleLogicUnitState.Dead, new BattleLogicUnitDeadState(BattleLogicUnitState.Dead, this));

    }

    private void AddEnemyState()
    {
        fsmDict.Add(BattleLogicUnitState.Spawn, new BattleLogicUnitSpawnState(BattleLogicUnitState.Spawn, this));
        fsmDict.Add(BattleLogicUnitState.Idle, new BattleLogicUnitIdleState_Enemy(BattleLogicUnitState.Idle, this));
        fsmDict.Add(BattleLogicUnitState.Move, new BattleLogicUnitMoveState_Enemy(BattleLogicUnitState.Move, this));
        fsmDict.Add(BattleLogicUnitState.Fight, new BattleLogicUnitFightState_Enemy(BattleLogicUnitState.Fight, this));
        fsmDict.Add(BattleLogicUnitState.Dead, new BattleLogicUnitDeadState(BattleLogicUnitState.Dead, this));

    }

    public override void Update()
    {
        CheckEnableFight();
        base.Update();

    }

    private void CheckEnableFight()
    {
        enableFight = BattleDataRunTimeHelper.GetCurBattleLogicState() == BattleLogicState.Attacking;
    }

    public bool HasAnySkillReleasing()
    {
        return srcUnit.skillManager.HasAnySkillReleasing();
    }

    public bool HasAnySkillCanRelease(float dis)
    {
        return srcUnit.skillManager.HasAnySkillCanRelease(dis);
    }

    public bool HasAnySkillWaitForRelease()
    {
        return srcUnit.skillManager.HasAnySkillWaitForRelease();
    }

    internal void OnGetInputKey(KeyCode key)
    {
        if (srcUnit.unitCamp == UnitCamp.Player)
        {
            curGetKeyCode = key;
        }
    }

    internal void OnPressKey(KeyCode key)
    {
        if(srcUnit.unitCamp == UnitCamp.Player && isOnGround)
        {
            curPressKeyCode = key;
        }
    }


}
