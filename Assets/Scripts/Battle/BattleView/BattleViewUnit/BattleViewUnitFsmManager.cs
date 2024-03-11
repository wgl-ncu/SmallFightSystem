using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BattleViewUnitState
{
    Spawn,
    Idle,
    Move,
    Fight,
    Dead
}
public class BattleViewUnitFsmManager : BaseFsmManager<BattleViewUnitState>
{
    public BattleViewUnitBase viewUnit;
    public BattleViewUnitFsmManager(BattleViewUnitBase viewUnit)
    {
        this.viewUnit = viewUnit;
        fsmDict = new Dictionary<BattleViewUnitState, BaseFsm<BattleViewUnitState>>();
        fsmDict.Add(BattleViewUnitState.Spawn, new BattleViewUnitSpawnState(BattleViewUnitState.Spawn, this));
        fsmDict.Add(BattleViewUnitState.Idle, new BattleViewUnitIdleState(BattleViewUnitState.Idle, this));
        fsmDict.Add(BattleViewUnitState.Move, new BattleViewUnitMoveState(BattleViewUnitState.Move, this));
        fsmDict.Add(BattleViewUnitState.Fight, new BattleViewUnitFightState(BattleViewUnitState.Fight, this));
        fsmDict.Add(BattleViewUnitState.Dead, new BattleViewUnitDeadState(BattleViewUnitState.Dead, this));
    }
}
