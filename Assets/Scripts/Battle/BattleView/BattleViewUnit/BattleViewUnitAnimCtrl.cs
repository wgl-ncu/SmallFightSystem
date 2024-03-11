using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleViewUnitAnimState
{
    NONE = -1,
    Spawn,
    Idle,
    Move,
    Fight,
    Dead,

}
public class BattleViewUnitAnimCtrl : MonoBehaviour
{
    private Animator animator;
    private BattleViewUnitAnimState curState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        curState = BattleViewUnitAnimState.NONE;
    }

    public void SwitchState(BattleViewUnitState state)
    {
        if (curState != (BattleViewUnitAnimState)state)
        {
            curState = (BattleViewUnitAnimState)state;
            ChangeAnim();
        }
    }

    private void ChangeAnim()
    {
        var trigger = BattleViewUnitAnimCtrl.AnimTrigger[(int)curState];
        animator.SetTrigger(trigger);
    }

    public void PlayAnim(string anim)
    {
        animator.SetTrigger(anim);
        curState = BattleViewUnitAnimState.NONE;
    }

    private static string[] AnimTrigger = { "spawn", "idle", "run", "attack", "die" };
}
