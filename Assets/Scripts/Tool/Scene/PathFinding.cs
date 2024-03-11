using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct MapTileRange
{
    public enum TileType
    {
        Ground,
        Road,
        Sky
    }
    public int start;
    public int end;
    public TileType type;
}
public static class PathFinding
{
    public static List<List<int>> GetMapData(SceneData_SO sceneData)
    {
        GetMaxPosAndMinPos(sceneData, out var min, out var max, out var heightList);
        int width = (int)(max.x - min.x + 1);
        int height = heightList.Count * 3 - 1;
        List<List<int>> map = new List<List<int>>();
        var ranges = GetLayer(heightList);
        for (int i = 0; i < width; ++i)
        {
            map.Add(new List<int>());
            for(int j = 0; j < height; ++j)
            {
                var layer = map[i];
                var type = ranges[j].type;
                //var topPoint = new Vector2(i - 9)
                layer[j] = type == MapTileRange.TileType.Road ? 0 : 1;
            }
        }
        return map;
    }

    private static List<MapTileRange> GetLayer(List<int> heights)
    {
        heights.Sort((a, b) => a - b);
        List<MapTileRange> res = new List<MapTileRange>();
        for (int i = 0; i < heights.Count - 1; ++i)
        {
            var h = heights[i];
            res.Add(new MapTileRange() { start = h, end = h, type = MapTileRange.TileType.Ground });
            res.Add(new MapTileRange() { start = h + 1, end = h + 1, type = MapTileRange.TileType.Road });
            res.Add(new MapTileRange() { start = h + 2, end = heights[i + 1] - 1, type = MapTileRange.TileType.Sky });
        }
        var height = heights[heights.Count - 1];
        res.Add(new MapTileRange() { start = height, end = height, type = MapTileRange.TileType.Ground });
        res.Add(new MapTileRange() { start = height + 1, end = height + 1, type = MapTileRange.TileType.Road });
        return res;
    }

    private static void GetMaxPosAndMinPos(SceneData_SO sceneData, out Vector2 min, out Vector2 max, out List<int> heightLisht)
    {
        min = new Vector2(int.MaxValue, int.MaxValue);
        max = new Vector2(int.MinValue, int.MinValue);
        heightLisht = new List<int>();
        var grounds = sceneData.grounds;
        for(int i = 0; i< grounds.Count; ++i)
        {
            var ground = grounds[i];
            min.x = Mathf.Min(ground.start.x, min.x);
            max.x = Mathf.Max(ground.start.x, max.x);

            min.y = Mathf.Min(ground.start.y, min.y);
            max.y = Mathf.Max(ground.start.y, max.y);

            min.x = Mathf.Min(ground.end.x, min.x);
            max.x = Mathf.Max(ground.end.x, max.x);

            min.y = Mathf.Min(ground.end.y, min.y);
            max.y = Mathf.Max(ground.end.y, max.y);

            if(heightLisht.FindIndex(a=>a == (int)ground.height) == -1)
            {
                heightLisht.Add((int)ground.height);
            }
        }
        var obstacles = sceneData.obstacles;
        for(int i = 0;i < obstacles.Count; ++i)
        {
            var obstacle = obstacles[i];
            min.x = Mathf.Min(obstacle.top.x, min.x);
            max.x = Mathf.Max(obstacle.top.x, max.x);

            min.y = Mathf.Min(obstacle.top.y, min.y);
            max.y = Mathf.Max(obstacle.top.y, max.y);

            min.x = Mathf.Min(obstacle.bottom.x, min.x);
            max.x = Mathf.Max(obstacle.bottom.x, max.x);

            min.y = Mathf.Min(obstacle.bottom.y, min.y);
            max.y = Mathf.Max(obstacle.bottom.y, max.y);
        }
    }
}
