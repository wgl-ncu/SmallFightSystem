using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VFXLoadHelper
{
    public static Dictionary<string, GameObject> resDict = new Dictionary<string, GameObject>();

    public static BattleVFX LoadBattleVFX(string path)
    {
        if(!resDict.TryGetValue(path, out var res))
        {
            var obj = Resources.Load<BattleVFX>(path);
            resDict.Add(path, obj.gameObject);
            return obj;
        }
        else
        {
            return res.GetComponent<BattleVFX>();
        }
    }
}
