using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneData_SO : ScriptableObject
{
    public int id;
    public string path;
    public List<Ground> grounds;
    public List<Obstacle> obstacles;
}

[Serializable]
public struct Ground
{
    public Vector2 start;
    public Vector2 end;
    public float height;
}

[Serializable]
public struct Obstacle
{
    public Vector2 top;
    public Vector2 bottom;
}