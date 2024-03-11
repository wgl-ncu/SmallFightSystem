using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Status", menuName = "DataCreate/Character Status/Data")]
public class CharacterData_SO : ScriptableObject
{
    public int id;
    public int hp;
    public int attack;
    public float spawnTime = 1f;
    public string assetPath;
    public float atkRange = 1f;
    public float speed = 1f;
    public int normalAtk;
    public float height = 5f;
    public List<int> skills;
}
