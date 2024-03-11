using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneObjSetType
{
    Ground,
    Obstacle,
}
public class BattleSceneDataBase : MonoBehaviour
{
    public int sceneId;
#if UNITY_EDITOR
    public void GetSceneObjectSetMsgs(out int id,out List<Ground> grounds, out List<Obstacle> obstacles)
    {
        id = this.sceneId;
        grounds = new List<Ground>();
        obstacles = new List<Obstacle>();
        var sets = GetComponentsInChildren<BattleSceneObjectSet>();
        foreach(var set in sets)
        {
            var edge = set.GetEdge();
            if(set.type == SceneObjSetType.Ground)
            {
                grounds.Add(EdgeToGround(edge));
            }
            else if(set.type == SceneObjSetType.Obstacle)
            {
                obstacles.Add(EdgeToObstacle(edge));
            }
        }
    }

    public Ground EdgeToGround(BattleSceneObjectSetEdge edge)
    {
        return new Ground() { start = edge.head, end = edge.tail, height = edge.head.y };
    }

    public Obstacle EdgeToObstacle(BattleSceneObjectSetEdge edge)
    {
        return new Obstacle() { top = edge.head, bottom = edge.tail };
    }
#endif
}
