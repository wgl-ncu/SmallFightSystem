using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateLoad : BaseFsm<GameState>
{
    private GameFsmManager gameFsmManager;
    public GameStateLoad(GameState state, GameFsmManager fsmManager) : base(state, fsmManager)
    {
        gameFsmManager = fsmManager;
    }
    public override void Enter()
    {
        gameFsmManager.loadFinish = false;
        EventManager.TriggerEvent(CommonEvent.ON_GAME_LOAD);
    }

    public override void Leave()
    {
        
    }

    public override void Update()
    {
        if (gameFsmManager.loadFinish)
        {
            _fsmManager.SwitchState(GameState.Idle);
        }
    }
}
