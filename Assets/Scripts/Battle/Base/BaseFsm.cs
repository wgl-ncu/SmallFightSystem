using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFsm<T> where T : Enum
{
    protected T _state;
    protected BaseFsmManager<T> _fsmManager;
    public BaseFsm(T state, BaseFsmManager<T> fsmManager)
    {
        _state = state;
        _fsmManager = fsmManager;
    }
    public abstract void Enter();

    public abstract void Leave();

    public abstract void Update();

    public T GetState()
    {
        return _state;
    }
}

public enum AEnum
{
    A,
    B
}
public class AFsm : BaseFsm<AEnum>
{
    public AFsm(AEnum state, BaseFsmManager<AEnum> fsmManager) : base(state, fsmManager)
    {


    }
    public override void Enter()
    {
        
    }

    public override void Leave()
    {
        
    }

    public override void Update()
    {
        
    }
}
