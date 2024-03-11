using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFsmLoadState : BaseFsm<BattleState>
{
    private BattleFsmManager fsmManager;
    public BattleFsmLoadState(BattleState state, BattleFsmManager fsmManager) : base(state, fsmManager)
    {
        this.fsmManager = fsmManager;
    }
    public override void Enter()//ս����λ�ļ�����logicfsm��ִ��
    {
        
    }

    public override void Leave()
    {
        
    }

    public override void Update()
    {
        fsmManager.SwitchState(BattleState.Fight);
    }
}
