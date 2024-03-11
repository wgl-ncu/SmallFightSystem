using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BattleSceneObjectSetEdge
{
    public Vector2 head;
    public Vector2 tail;
}
public class BattleSceneObjectSet : MonoBehaviour
{
    public SceneObjSetType type;

    public BattleSceneObjectSetEdge GetEdge()
    {
        var objs = GetComponentsInChildren<BattleSceneObject>();
        int max = int.MinValue;
        int min = int.MaxValue;
        int subValue;
        foreach(var obj in objs)
        {
            if(type == SceneObjSetType.Ground)
            {
                int pos = (int)obj.GetPos().x;
                min = Mathf.Min(min, pos);
                max = Mathf.Max(max, pos);
            }
        }
        Vector2 minValue = default;
        Vector2 maxValue = default;
        if(type == SceneObjSetType.Ground)
        {
            subValue = (int)(objs[0].GetPos().y + 0.5f);
            minValue = new Vector2(min, subValue);
            maxValue = new Vector2(max, subValue);
        }
        var edge = new BattleSceneObjectSetEdge() { head = minValue, tail = maxValue };
        return edge;
    }
}
