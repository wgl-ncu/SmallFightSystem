using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    [HideInInspector] public string pageName;
    public virtual void Open(object param = null)
    {

    }

    public virtual void Close()
    {
        UIManager.Instance.ClosePage(this);
    }

    public virtual void OnClose()
    {

    }
}
