using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    Idle,
    Load,
    Fight,
    Finish
}
public class BattleFsmManager : BaseFsmManager<BattleState>
{
    public BattleManager battleManager;
    public BattleFsmManager(BattleManager battleManager)
    {
        this.battleManager = battleManager;
        fsmDict = new Dictionary<BattleState, BaseFsm<BattleState>>();
        fsmDict.Add(BattleState.Idle, new BattleFsmIdleState(BattleState.Idle, this));
        fsmDict.Add(BattleState.Load, new BattleFsmLoadState(BattleState.Load, this));
        fsmDict.Add(BattleState.Fight, new BattleFsmFightState(BattleState.Fight, this));
        fsmDict.Add(BattleState.Finish, new BattleFsmFinishState(BattleState.Finish, this));
    }

    public override void SwitchState(BattleState state)
    {
        base.SwitchState(state);
        Logger.Log("ÇÐ»»Õ½¶·×´Ì¬£º" + state, null, LogLayer.Battle);
    }
}
