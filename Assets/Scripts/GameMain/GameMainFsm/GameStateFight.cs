using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateFight : BaseFsm<GameState>
{
    private GameFsmManager mgr;
    public GameStateFight(GameState state, GameFsmManager fsmManager) : base(state, fsmManager)
    {
        mgr = fsmManager;
    }
    public override void Enter()
    {
        
    }

    public override void Leave()
    {
        
    }

    public override void Update()
    {
        BattleManager.Instance.Update(mgr.DeltaTime);
    }
}
