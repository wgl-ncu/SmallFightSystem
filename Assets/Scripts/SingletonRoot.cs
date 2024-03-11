using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonRoot : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
