using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleLogicState
{
    Idle,
    Load,
    Enter,
    Attacking,
    Exit
}
public class BattleLogicFsmManager : BaseFsmManager<BattleLogicState>
{
    public BattleLogicManager battleLogicManager;

    public bool levelLoadFinish = false;
    public BattleLogicFsmManager(BattleLogicManager battleLogicManager)
    {
        this.battleLogicManager = battleLogicManager;
        fsmDict = new Dictionary<BattleLogicState, BaseFsm<BattleLogicState>>();
        fsmDict.Add(BattleLogicState.Idle, new BattleLogicIdleState(BattleLogicState.Idle, this));
        fsmDict.Add(BattleLogicState.Load, new BattleLogicLoadState(BattleLogicState.Load, this));
        fsmDict.Add(BattleLogicState.Enter, new BattleLogicEnterState(BattleLogicState.Enter, this));
        fsmDict.Add(BattleLogicState.Attacking, new BattleLogicAttackingState(BattleLogicState.Attacking, this));
        fsmDict.Add(BattleLogicState.Exit, new BattleLogicExitState(BattleLogicState.Exit, this));
        SwitchState(BattleLogicState.Idle);
    }

    internal void OnLeaveLoadState()
    {
        //battleLogicManager.StartUnits();
    }

    public void OnEnterLoadState()
    {
        battleLogicManager.LoadLevelData();
    }

    public List<int> GetEnemyIds()
    {
        return battleLogicManager.GetEnemyIds();
    }
}
