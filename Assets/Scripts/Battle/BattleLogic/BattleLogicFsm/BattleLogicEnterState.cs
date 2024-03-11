using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicEnterState : BaseFsm<BattleLogicState>
{
    private float passTime;
    private float waitTime;
    public BattleLogicEnterState(BattleLogicState state, BattleLogicFsmManager fsmManager):base(state, fsmManager)
    {
        
    }

    public override void Enter()//½øÈë¹Ø¿¨
    {
        waitTime = BattleDataRunTimeHelper.GetBattleConfig().levelEnterTime;
        passTime = 0;
        EventManager.TriggerEvent(CommonEvent.ON_ENTER_BATTLE_LOGIC_ENTER_STATE);
    }

    public override void Leave()
    {
        
    }

    public override void Update()
    {
        passTime += BattleManager.Instance._deltaTime;
        if(passTime >= waitTime)
        {
            _fsmManager.SwitchState(BattleLogicState.Attacking);
        }
    }
}
