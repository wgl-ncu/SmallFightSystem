using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Battle Config", menuName = "DataCreate/BattleCongfig/Config")]
public class BattleConfig_SO : ScriptableObject
{
    public float levelEnterTime = 4f;
    public float planePos = -3f;
}
[Serializable]
public class KeyValueItem<T, U>
{
    public T key;
    public U value;
}
