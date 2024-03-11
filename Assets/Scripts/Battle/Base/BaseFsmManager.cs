using System;
using System.Collections.Generic;

public class BaseFsmManager<T> where T : Enum
{
    public Dictionary<T, BaseFsm<T>> fsmDict;
    public BaseFsm<T> curFsm;

    public T CurState
    {
        get;
        protected set;
    }

    public BaseFsmManager()
    {

    }

    public virtual void Update()
    {
        curFsm.Update();
    }

    public virtual void SwitchState(T state)
    {
        if(null != curFsm)
        {
            curFsm.Leave();
        }
        
        if(fsmDict.TryGetValue(state, out var baseFsm))
        {
            CurState = state;
            curFsm = baseFsm;
            baseFsm.Enter();
        }
    }
}

public class AFsmManager : BaseFsmManager<AEnum>
{

}
