using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Load,
    Idle,
    Fight
}

public class GameFsmManager : BaseFsmManager<GameState>
{
    public GameMain gameMain;

    public float DeltaTime => gameMain.deltaTime;

    public bool loadFinish;
    public GameFsmManager(GameMain gameMain)
    {
        this.gameMain = gameMain;
        fsmDict = new Dictionary<GameState, BaseFsm<GameState>>();
        fsmDict.Add(GameState.Load, new GameStateLoad(GameState.Load, this));
        fsmDict.Add(GameState.Idle, new GameStateIdle(GameState.Idle, this));
        fsmDict.Add(GameState.Fight, new GameStateFight(GameState.Fight, this));
    }
}
