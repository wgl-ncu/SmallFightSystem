using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneGround
{
    public Vector2 start;
    public Vector2 end;
    public float height;
    public bool startCanJump;
    public bool endCanJump;
    public SceneGround(Ground g)
    {
        start = g.start;
        end = g.end;
        height = g.height;
        startCanJump = false;
        endCanJump = false;
    }
}
public class BattleLogicSceneManager
{
    private BattleLogicManager battleLogicManager;
    public SceneData_SO data;
    public List<SceneGround> sceneGrounds;
    public BattleLogicSceneManager(BattleLogicManager battleLogicManager)
    {
        this.battleLogicManager = battleLogicManager;
        sceneGrounds = new List<SceneGround>();
    }

    public void LoadSceneData(SceneData_SO data)
    {
        this.data = data;
        sceneGrounds.Clear();
        for(int i = 0; i<data.grounds.Count; ++i)
        {
            var g = data.grounds[i];
            sceneGrounds.Add(new SceneGround(g));
        }
        RefreshSceneData();
    }

    private void RefreshSceneData()
    {
        for (int i = 0; i < sceneGrounds.Count; ++i)
        {
            var g = sceneGrounds[i];
            var h = g.height;
            var edge1 = g.start.x;
            var edge2 = g.end.x;
            if (sceneGrounds.FindIndex(a => a.height < h && edge1 < a.end.x && edge1 > a.start.x) != -1)
            {
                g.startCanJump = true;
            }
            if (sceneGrounds.FindIndex(a => a.height < h && edge2 < a.end.x && edge2 > a.start.x) != -1)
            {
                g.endCanJump = true;
            }
        }
    }

    public bool IsOnGround(Vector3 pos)
    {
        if(null == data)
        {
            return false;
        }
        var height = pos.y;
        var possibleGrounds = data.grounds.FindAll(a => a.height == height);
        for(int i = 0; i < possibleGrounds.Count; ++i)
        {
            var ground = possibleGrounds[i];
            if(pos.x > ground.start.x && pos.x < ground.end.x)
            {
                return true;
            }
        }
        return false;
    }

    public bool TryGetStandGround(Vector3 pos, out SceneGround ground)
    {
        if (null == data)
        {
            ground = null;
            return false;
        }
        var height = pos.y;
        var possibleGrounds = sceneGrounds.FindAll(a => a.height == height);
        for (int i = 0; i < possibleGrounds.Count; ++i)
        {
            var g = possibleGrounds[i];
            if (pos.x >= g.start.x && pos.x <= g.end.x)
            {
                ground = g;
                return true;
            }
        }
        ground = null;
        return false;
    }

    internal float GetNearestBottomGroundHeight(Vector3 pos)
    {
        var possibleGrounds = data.grounds.FindAll(a => a.start.x <= pos.x && a.end.x >= pos.x && a.height <= pos.y);
        float res = 0;
        if (possibleGrounds.Count > 0)
        {
            res = possibleGrounds[0].height;
            for (int i = 1; i < possibleGrounds.Count; ++i)
            {
                res = res >= data.grounds[i].height ? res : data.grounds[i].height;
            }
        }
        return res;
    }
}
