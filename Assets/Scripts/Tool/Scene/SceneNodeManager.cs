using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNodeManager : SingletonCSharp<SceneNodeManager>
{
    private Transform _UIRoot;
    public Transform UIRoot
    {
        get
        {
            if(null == _UIRoot)
            {
                _UIRoot = GameObject.Find("UIRoot").transform;
            }
            return _UIRoot;
        }
    }

    private Transform _ViewUnitRoot;
    public Transform ViewUnitRoot
    {
        get
        {
            if(null == _ViewUnitRoot)
            {
                _ViewUnitRoot = GameObject.Find("ViewUnitRoot").transform;
            }
            return _ViewUnitRoot;
        }
    }

    private Transform _DamageRoot;

    public Transform DamageRoot
    {
        get
        {
            if (null == _DamageRoot)
            {
                _DamageRoot = GameObject.Find("DamageNumRoot").transform;
            }
            return _DamageRoot;
        }
    }

    private Transform _VFXRoot;
    public Transform VFXRoot
    {
        get
        {
            if (null == _VFXRoot)
            {
                _VFXRoot = GameObject.Find("VFXRoot").transform;
            }
            return _VFXRoot;
        }
    }

    private Transform _SceneRoot;

    public Transform SceneRoot
    {
        get
        {
            if(null == _SceneRoot)
            {
                _SceneRoot = GameObject.Find("SceneRoot").transform;
            }
            return _SceneRoot;
        }
    }

    public void RefreshOrderLayer()
    {
        ViewUnitRoot.GetComponent<OrderLayerAuto>().Refresh();
    }
}
