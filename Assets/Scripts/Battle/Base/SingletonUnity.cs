using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonUnity<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = new GameObject("SingletonUnity_" + typeof(T));
                instance = obj.AddComponent<T>();
                obj.transform.parent = GameObject.Find("SingletonRoot").transform;
            }
            return instance;
        }
    }
}
